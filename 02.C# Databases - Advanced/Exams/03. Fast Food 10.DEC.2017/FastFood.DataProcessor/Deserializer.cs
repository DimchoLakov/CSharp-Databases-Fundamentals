using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";
        private const string SuccessfullyAddedOrder = "Order for {0} on {1} added";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var employees = new List<Employee>();

            var sb = new StringBuilder();

            foreach (var employeeDto in deserializedEmployees)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var employee = Mapper.Map<Employee>(employeeDto);

                var position = context
                    .Positions
                    .FirstOrDefault(p => p.Name == employeeDto.Position);

                if (position == null)
                {
                    position = new Position()
                    {
                        Name = employeeDto.Position
                    };

                    context
                        .Positions
                        .Add(position);

                    context
                        .SaveChanges();
                }

                employee.Position = position;
                employees.Add(employee);

                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context
                .Employees
                .AddRange(employees);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var items = new List<Item>();

            var sb = new StringBuilder();

            foreach (var itemDto in deserializedItems)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var itemExists = items.Any(i => i.Name == itemDto.Name);

                if (itemExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var category = GetOrCreateCategory(context, itemDto.Category);

                var item = Mapper.Map<Item>(itemDto);
                item.Category = category;
                items.Add(item);

                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }

            context
                .Items
                .AddRange(items);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        private static Category GetOrCreateCategory(FastFoodDbContext context, string categoryName)
        {
            var category = context
                .Categories
                .FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                category = new Category
                {
                    Name = categoryName
                };

                context.Categories.Add(category);
                context.SaveChanges();
            }

            return category;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var deserializedOrders = (OrderDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var orders = new List<Order>();

            foreach (var orderDto in deserializedOrders)
            {
                var employee = context
                                         .Employees
                                         .FirstOrDefault(e => e.Name == orderDto.Employee);

                var employeeExists = employee != null;

                if (!IsValid(orderDto) || !employeeExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var orderItems = new List<OrderItem>();

                var hasInvalidItems = false;

                foreach (var oiDto in orderDto.Items)
                {
                    var item = context.Items.FirstOrDefault(i => i.Name == oiDto.ItemName);

                    if (item == null)
                    {
                        hasInvalidItems = true;
                        break;
                    }

                    var orderItem = new OrderItem()
                    {
                        Item = item,
                        Quantity = oiDto.Quantity
                    };

                    orderItems.Add(orderItem);
                }

                if (hasInvalidItems)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isTypeValid = Enum.TryParse(orderDto.Type, true, out OrderType orderType);

                if (!isTypeValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var dateTime = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var order = new Order()
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    DateTime = dateTime,
                    Type = orderType,
                    OrderItems = orderItems
                };

                orders.Add(order);

                sb.AppendLine(string.Format(SuccessfullyAddedOrder, orderDto.Customer, orderDto.DateTime));
            }

            context
                .Orders
                .AddRange(orders);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj), new List<ValidationResult>(), true);
        }
    }
}