using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductShop.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.App.DTOs.Export;
using ProductShop.App.DTOs.Import;
using ProductShop.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace ProductShop.App
{
    public class XmlProcessor
    {
        private const string XmlImportPath = "./../../../Resources/{0}";
        private const string UsersPath = "users.xml";
        private const string ProductsPath = "products.xml";
        private const string CategoriesPath = "categories.xml";

        private const string XmlExportPath = "./../../../XmlExport/{0}";
        private const string ProductsInRangePath = "productsInRange.xml";
        private const string UsersSoldProductsPath = "users-sold-products.xml";
        private const string CategoriesByProductCountPath = "categoriesByCount.xml";
        private const string UsersAndProductsPath = "usersAndProducts.xml";

        private readonly ProductShopDbContext _dbContext;

        public XmlProcessor()
        {
            this._dbContext = new ProductShopDbContext();
        }

        public void ImportData()
        {
            MapperInitializer.InitializeMapper();

            this._dbContext
                .Database
                .Migrate();

            this.ImportUsers();
            this.ImportProducts();
            this.ImportCategories();
            this.AddCategoryProducts();
        }

        public void ExportData()
        {
            this.ExportProductsInRange();
            this.ExportSoldProducts();
            this.ExportCategoriesByProductCount();
            this.ExportUsersAndProducts();
        }

        private void ExportUsersAndProducts()
        {
            var users = new UserCollectionDto()
            {
                Count = this._dbContext.Users.Count(),
                UserDtos = this._dbContext
                    .Users
                    .Where(u => u.ProductsSold.Count > 0)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .Select(x => new DTOs.Export.UserDto()
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Age = x.Age.ToString(),
                        SoldProducts = new SoldProductsCollectionDto()
                        {
                            Count = x.ProductsSold.Count,
                            SoldProductDtos = x.ProductsSold
                                .Select(z => new SoldProductAttributesDto()
                                {
                                    Name = z.Name,
                                    Price = z.Price
                                }).ToArray()
                        }
                    }).ToArray()
            };

            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(UserCollectionDto), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText(string.Format(XmlExportPath, UsersAndProductsPath), sb.ToString().Trim());
        }

        private void ExportCategoriesByProductCount()
        {
            var categoriesDtos = this._dbContext
                .Categories
                .Select(c => new CategoryByProductsCountDto()
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Select(x => x.Product.Price).DefaultIfEmpty(0).Average(),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderBy(x => x.ProductsCount)
                .ToArray();

            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(CategoryByProductsCountDto[]), new XmlRootAttribute("categories"));
            serializer.Serialize(new StringWriter(sb), categoriesDtos, xmlNamespaces);

            File.WriteAllText(string.Format(XmlExportPath, CategoriesByProductCountPath), sb.ToString().Trim());
        }

        private void ExportSoldProducts()
        {
            var users = this._dbContext
                .Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(x => new ExportUserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                        .Select(ps => new SoldProductDto()
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .ToArray()
                })
                .ToArray();

            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText(string.Format(XmlExportPath, UsersSoldProductsPath), sb.ToString().Trim());
        }

        private void ExportProductsInRange()
        {
            var xmlSerializer = new XmlSerializer(typeof(ProductInRangeDto[]), new XmlRootAttribute("products"));

            var productsInRangeDtos = this._dbContext
                .Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                .OrderBy(p => p.Price)
                .Select(x => new ProductInRangeDto()
                {
                    Name = x.Name,
                    Price = x.Price,
                    BuyerFullName = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .ToArray();

            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(sb), productsInRangeDtos, xmlNamespaces);

            File.WriteAllText(string.Format(XmlExportPath, ProductsInRangePath), sb.ToString().Trim());
        }

        private void AddCategoryProducts()
        {
            var categoriesCount = 11;
            var productsCount = 200;

            var categoryProducts = new List<CategoryProduct>();

            for (int i = 1; i <= productsCount; i++)
            {
                var categoryId = new Random().Next(1, categoriesCount);
                var productId = i;

                var categoryProduct = new CategoryProduct()
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
            }

            this._dbContext
                .CategoryProducts
                .AddRange(categoryProducts);

            this._dbContext
                .SaveChanges();
        }

        private void ImportCategories()
        {
            var categoriesXmlString = File.ReadAllText(string.Format(XmlImportPath, CategoriesPath));

            var xmlSerialier = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializedCategories = (CategoryDto[])xmlSerialier.Deserialize(new StringReader(categoriesXmlString));

            var categories = new List<Category>();

            foreach (var categoryDto in deserializedCategories)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                var category = Mapper.Map<Category>(categoryDto);

                categories.Add(category);
            }

            this._dbContext
                .Categories
                .AddRange(categories);

            this._dbContext
                .SaveChanges();
        }

        private void ImportProducts()
        {
            var productsXmlString = File.ReadAllText(string.Format(XmlImportPath, ProductsPath));

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var deserializedProducts = (ProductDto[])serializer.Deserialize(new StringReader(productsXmlString));

            var products = new List<Product>();

            var counter = 1;

            foreach (var productDto in deserializedProducts)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                var product = Mapper.Map<Product>(productDto);

                product.BuyerId = new Random().Next(1, 31);
                product.SellerId = new Random().Next(30, 57);

                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                counter++;

                products.Add(product);
            }

            this._dbContext
                .Products
                .AddRange(products);

            this._dbContext.SaveChanges();
        }

        private void ImportUsers()
        {
            var xmlString = File.ReadAllText(string.Format(XmlImportPath, UsersPath));

            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("users"));
            var deserializerdUsers = (ImportUserDto[])serializer.Deserialize(new StringReader(xmlString));

            var users = new List<User>();

            foreach (var importUserDto in deserializerdUsers)
            {
                if (!IsValid(importUserDto))
                {
                    continue;
                }

                var user = Mapper.Map<User>(importUserDto);

                users.Add(user);
            }

            this._dbContext.Database.Migrate();
            this._dbContext
                .Users
                .AddRange(users);
            this._dbContext.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
