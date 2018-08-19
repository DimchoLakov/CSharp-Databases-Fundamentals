using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.App.Dto.Import;
using ProductShop.Data;
using ProductShop.Models;
using System.Linq;
using ProductShop.App.Dto.Export;
using Mapper = AutoMapper.Mapper;

namespace ProductShop.App
{
    public class JsonProcessor
    {
        private const string ImportPath = "./../../../Json/{0}";
        private const string UsersPath = "users.json";
        private const string ProductsPath = "products.json";
        private const string CategoriesPath = "categories.json";

        private const string ExportPath = "./../../../Export/{0}";
        private const string ProductsInRangePath = "products-in-range.json";
        private const string SuccessfullySoldProductsPath = "users-sold-products.json";
        private const string CategoriesByProductCountPath = "categories-by-products.json";
        private const string UsersAndProductsPath = "users-and-products.json";

        private readonly ProductShopContext _dbContext;

        public JsonProcessor()
        {
            this._dbContext = new ProductShopContext();
        }

        public void InitializeDatabase()
        {
            this._dbContext
                .Database
                .Migrate();
        }

        public void ImportData()
        {
            MapperInitializer.InitializeMapper();
            this.ImportUsers();
            this.ImportProducts();
            this.ImportCategories();
            this.ImportCategoryProducts();
        }

        public void ExportData()
        {
            this.ExportProductsInRange();
            this.ExportSuccessfullySoldProducts();
            this.ExportCategoriesByProductCount();
            this.ExportUsersAndProducts();
        }

        private void ExportUsersAndProducts()
        {
            var users = new UserCollectionDto()
            {
                Count = this._dbContext
                    .Users
                    .Count(u => u.ProductsSold.Count > 0),
                Users = this._dbContext
                    .Users
                    .Select(u => new UserDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new SoldProductCollectionDto()
                        {
                            Count = u.ProductsSold.Count,
                            Products = u.ProductsSold
                                .Select(ps => new SoldProductDto()
                                {
                                    Name = ps.Name,
                                    Price = ps.Price
                                }).ToArray()
                        }
                    })
                    .OrderByDescending(x => x.SoldProducts.Count)
                    .ThenBy(x => x.LastName)
                    .ToArray()
            };

            var jsonSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedUsersString = JsonConvert.SerializeObject(users, Formatting.Indented, jsonSettings);

            File.WriteAllText(string.Format(ExportPath, UsersAndProductsPath), serializedUsersString);
        }

        private void ExportCategoriesByProductCount()
        {
            var categories = this._dbContext
                .Categories
                .Select(c => new ExportCategoryDto()
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Average(),
                    TotalRevenue = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Sum()
                })
                .OrderByDescending(c => c.ProductsCount)
                .ToArray();

            var jsonSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var categoriesString = JsonConvert.SerializeObject(categories, Formatting.Indented, jsonSettings);

            File.WriteAllText(string.Format(ExportPath, CategoriesByProductCountPath), categoriesString);
        }

        private void ExportSuccessfullySoldProducts()
        {
            var usersSoldProducts = this._dbContext
                .Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(x => new ExportUserSoldProductsDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                        .Select(sp => new ExportSoldProductDto()
                        {
                            Name = sp.Name,
                            Price = sp.Price,
                            BuyerFirstName = sp.Buyer.FirstName,
                            BuyerLastName = sp.Buyer.LastName
                        })
                        .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToArray();

            var jsonSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var result = JsonConvert.SerializeObject(usersSoldProducts, Formatting.Indented, jsonSettings);

            File.WriteAllText(string.Format(ExportPath, SuccessfullySoldProductsPath), result);
        }

        private void ExportProductsInRange()
        {
            var productsInRange = this._dbContext
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new ExportProductDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = p.Seller.FirstName == null ? string.Empty : p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToArray();

            var jsonSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedObjects = JsonConvert.SerializeObject(productsInRange, Formatting.Indented, jsonSettings);

            File.WriteAllText(string.Format(ExportPath, ProductsInRangePath), serializedObjects);
        }

        private void ImportCategoryProducts()
        {
            var categoriesCount = this._dbContext
                .Categories
                .Count();

            var productsCount = this._dbContext
                .Products
                .Count();

            var categoryProducts = new List<CategoryProduct>();
            var random = new Random();

            for (int i = 1; i <= productsCount; i++)
            {
                var categoryProduct = new CategoryProduct()
                {
                    ProductId = i,
                    CategoryId = random.Next(1, categoriesCount + 1)
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
            var categoriesString = File.ReadAllText(string.Format(ImportPath, CategoriesPath));
            var deserializedCategories = JsonConvert.DeserializeObject<ImportCategoryDto[]>(categoriesString);

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
                .AddRange(categories);

            this._dbContext
                .SaveChanges();
        }

        private void ImportProducts()
        {
            var productsString = File.ReadAllText(string.Format(ImportPath, ProductsPath));

            var deserializedProducts = JsonConvert.DeserializeObject<ImportProductDto[]>(productsString);

            var products = new List<Product>();

            var random = new Random();

            var usersCount = this._dbContext
                .Users
                .Count();

            var counter = 1;

            foreach (var productDto in deserializedProducts)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                var product = Mapper.Map<Product>(productDto);

                product.SellerId = random.Next(1, usersCount / 2);
                product.BuyerId = random.Next(usersCount / 2, usersCount + 1);

                products.Add(product);

                if (counter == 3)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                counter++;
            }

            this._dbContext
                .Products
                .AddRange(products);

            this._dbContext
                .SaveChanges();
        }

        private void ImportUsers()
        {
            using (var sr = new StreamReader(string.Format(ImportPath, UsersPath)))
            {
                JsonReader jsonReader = new JsonTextReader(sr);

                var jsonSerializer = new JsonSerializer();

                var userDtos = jsonSerializer.Deserialize<ImportUserDto[]>(jsonReader);

                var users = new List<User>();

                foreach (var userDto in userDtos)
                {
                    if (!IsValid(userDto))
                    {
                        continue;
                    }

                    var user = Mapper.Map<User>(userDto);

                    users.Add(user);
                }

                this._dbContext
                    .Users
                    .AddRange(users);

                this._dbContext
                    .SaveChanges();
            }
        }

        public static bool IsValid(object obj)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj), new List<ValidationResult>(), true);
        }
    }
}
