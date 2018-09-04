using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var enumType = Enum.Parse<OrderType>(orderType);

            var result = new ExpOrderDto()
            {
                Name = employeeName,
                Orders = context
                    .Orders
                    .Where(o => o.Employee.Name == employeeName && o.Type == enumType)
                    .Select(o => new ExpOrderItemDto()
                    {
                        Customer = o.Customer,

                        Items = o.OrderItems.
                            Select(i => new ExpItemDto()
                            {
                                Name = i.Item.Name,
                                Price = i.Item.Price,
                                Quantity = i.Quantity
                            })
                            .ToArray(),

                        TotalPrice = o.OrderItems.Sum(x => x.Item.Price * x.Quantity)
                    })
                    .ToArray(),

                TotalMade = context.OrderItems
                                   .Where(oi => oi.Order.Employee.Name == employeeName &&
                                                           oi.Order.Type == enumType)
                                   .Sum(o => o.Item.Price * o.Quantity)
            };

            var serializedOrders = JsonConvert.SerializeObject(result, Formatting.Indented);

            return serializedOrders;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categories = categoriesString.Split(",");

            var allCategories = context
                .Categories
                .Where(c => categories.Any(x => c.Name == x))
                .Select(c => new ExpCategoryDto()
                {
                    Name = c.Name,
                    MostPopularItem = c.Items
                                       .OrderByDescending(i => i.OrderItems.Sum(s => s.Quantity * s.Item.Price))
                                       .Select(m => new ExpMostPopularItemDto()
                                       {
                                           Name = m.Name,
                                           TimesSold = m.OrderItems.Sum(oi => oi.Quantity),
                                           TotalMade = m.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price)
                                       })
                                        .FirstOrDefault()
                })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenBy(x => x.MostPopularItem.TimesSold)
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExpCategoryDto[]), new XmlRootAttribute("Categories"));
            var xmlNameSpaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), allCategories, xmlNameSpaces);

            return sb.ToString().Trim();
        }
    }
}