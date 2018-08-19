using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarDealer.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.App.DTOs.Export;
using CarDealer.App.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace CarDealer.App
{
    public class JsonProcessor
    {
        private const string ImportPath = "./../../../Json/{0}";
        private const string SuppliersPath = "suppliers.json";
        private const string PartsPath = "parts.json";
        private const string CarsPath = "cars.json";
        private const string CustomersPath = "customers.json";

        private const string ExportPath = "./../../../ExportFiles/{0}";
        private const string OrderedCustomersPath = "ordered-customers.json";
        private const string ToyotaCarsPath = "toyota-cars.json";
        private const string LocalSuppliersPath = "local-suppliers.json";
        private const string CarPartsPath = "cars-and-parts.json";
        private const string TotalSalesCustomerPath = "customer-total-sales";
        private const string SalesDiscountPath = "sales-discount.json";

        private readonly CarDealerDbContext _dbContext;

        public JsonProcessor()
        {
            this._dbContext = new CarDealerDbContext();
        }

        public void MigrateDatabase()
        {
            this._dbContext
                .Database
                .Migrate();
        }

        public void ImportData()
        {
            MapperInitializer.InitializeMapper();
            this.ImportSuppliers();
            this.ImportParts();
            this.ImportCars();
            this.ImportCustomers();
            this.ImportSales();
        }

        private void ImportSales()
        {
            var discounts = new decimal[] { 0m, 0.5m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };

            var carsCount = this._dbContext.Cars.Count();
            var customersCount = this._dbContext.Customers.Count();

            var random = new Random();

            var sales = new List<Sale>();

            for (int i = 0; i < 3000; i++)
            {
                var sale = new Sale()
                {
                    CarId = random.Next(1, carsCount + 1),
                    CustomerId = random.Next(1, customersCount + 1),
                    Discount = discounts[random.Next(0, discounts.Length)]
                };

                sales.Add(sale);
            }

            this._dbContext
                .Sales
                .AddRange(sales);

            this._dbContext
                .SaveChanges();
        }

        private void ImportCustomers()
        {
            var customersString = File.ReadAllText(string.Format(ImportPath, CustomersPath));

            var deserializedCustomers = JsonConvert.DeserializeObject<ImportCustomerDto[]>(customersString);

            var customers = new List<Customer>();

            foreach (var customerDto in deserializedCustomers)
            {
                if (!IsValid(customerDto))
                {
                    continue;
                }

                var customer = Mapper.Map<Customer>(customerDto);

                customers.Add(customer);
            }

            this._dbContext
                .Customers
                .AddRange(customers);

            this._dbContext
                .SaveChanges();
        }

        private void ImportCars()
        {
            var carsString = File.ReadAllText(string.Format(ImportPath, CarsPath));

            var deserializedCars = JsonConvert.DeserializeObject<ImportCarDto[]>(carsString);

            var cars = new List<Car>();

            foreach (var carDto in deserializedCars)
            {
                if (!IsValid(carDto))
                {
                    continue;
                }

                var car = Mapper.Map<Car>(carDto);

                cars.Add(car);
            }

            this._dbContext.Cars.AddRange(cars);
            this._dbContext.SaveChanges();

            var partsCount = this._dbContext.Parts.Count();
            var partCars = this.GeneratePartCars(partsCount);

            this._dbContext.PartCars.AddRange(partCars);
            this._dbContext.SaveChanges();
        }

        private PartCar[] GeneratePartCars(int partsCount)
        {
            var random = new Random();

            var minParts = 10;
            var maxParts = 20;

            var count = random.Next(minParts, maxParts + 1);

            var carParts = new List<PartCar>();

            var cars = this._dbContext
                .Cars
                .ToList();

            foreach (var car in cars)
            {
                var randomPartIdsPerCar = new List<int>();

                for (int i = 0; i < count; i++)
                {
                    var randomPartId = random.Next(1, partsCount + 1);

                    if (randomPartIdsPerCar.Contains(randomPartId))
                    {
                        i--;
                        continue;
                    }

                    randomPartIdsPerCar.Add(randomPartId);

                    var carPart = new PartCar()
                    {
                        CarId = car.CarId,
                        PartId = randomPartId
                    };

                    carParts.Add(carPart);
                }
            }

            return carParts.ToArray();
        }

        private void ImportParts()
        {
            var partsString = File.ReadAllText(string.Format(ImportPath, PartsPath));

            var deserializedParts = JsonConvert.DeserializeObject<ImportPartDto[]>(partsString);

            var parts = new List<Part>();

            var suppliersCount = this._dbContext.Suppliers.Count();

            var random = new Random();

            foreach (var partDto in deserializedParts)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                var part = Mapper.Map<Part>(partDto);

                part.SupplierId = random.Next(1, suppliersCount + 1);

                parts.Add(part);
            }

            this._dbContext
                .Parts
                .AddRange(parts);

            this._dbContext
                .SaveChanges();
        }

        private void ImportSuppliers()
        {
            var suppliersString = File.ReadAllText(string.Format(ImportPath, SuppliersPath));

            var supplierDtos = JsonConvert.DeserializeObject<ImportSupplierDto[]>(suppliersString);

            var suppliers = new List<Supplier>();

            foreach (var supplierDto in supplierDtos)
            {
                if (!IsValid(supplierDto))
                {
                    continue;
                }

                var supplier = Mapper.Map<Supplier>(supplierDto);

                suppliers.Add(supplier);
            }

            this._dbContext
                .Suppliers
                .AddRange(suppliers);

            this._dbContext
                .SaveChanges();
        }

        public void ExportData()
        {
            this.ExportOrderedCustomers();
            this.ExportToyotaCars();
            this.ExportLocalSuppliers();
            this.ExportCarsWithTheirParts();
            this.ExportTotalSalesByCustomer();
            this.ExportSalesWithDiscount();
        }

        private void ExportSalesWithDiscount()
        {
            var sales = this._dbContext
                .Sales
                .Select(s => new SaleDto()
                {
                    Car = new CarSaleDto()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car
                                .PartCars
                                .Select(pc => pc.Part.Price)
                                .DefaultIfEmpty(0)
                                .Sum(),
                    PriceWithDiscount = s.Car
                                            .PartCars
                                            .Select(pc => pc.Part.Price)
                                            .DefaultIfEmpty(0)
                                            .Sum() * (1 - s.Discount)
                })
                .ToArray();

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedSales =
                JsonConvert.SerializeObject(sales, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText(string.Format(ExportPath, SalesDiscountPath), serializedSales);
        }

        private void ExportTotalSalesByCustomer()
        {
            var customers = this._dbContext
                .Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new CustomerSaleDto()
                {
                    FullName = c.Name,
                    BoughtCarsCount = c.Sales.Count,
                    SpentMoney = c.Sales
                        .Select(s => s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 + s.Discount))
                        .DefaultIfEmpty(0)
                        .Sum()
                        .ToString("f2")
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCarsCount)
                .ToArray();

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedCustomers =
                JsonConvert.SerializeObject(customers, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText(string.Format(ExportPath, TotalSalesCustomerPath), serializedCustomers);
        }

        private void ExportCarsWithTheirParts()
        {
            var cars = this._dbContext
                .Cars
                .Select(c => new
                {
                    car = new CarPartDto()
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance,
                        Parts = c.PartCars
                            .Select(pc => new PartDto()
                            {
                                Name = pc.Part.Name,
                                Price = pc.Part.Price
                            })
                            .ToArray()
                    }
                })
                .ToArray();

            var jsonSerializerSetings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedCarParts = JsonConvert.SerializeObject(cars, Formatting.Indented, jsonSerializerSetings);

            File.WriteAllText(string.Format(ExportPath, CarPartsPath), serializedCarParts);
        }

        private void ExportLocalSuppliers()
        {
            var localSuppliers = this._dbContext
                .Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new SupplierDto()
                {
                    Id = s.SupplierId,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedSuppliers =
                JsonConvert.SerializeObject(localSuppliers, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText(string.Format(ExportPath, LocalSuppliersPath), serializedSuppliers);
        }

        private void ExportToyotaCars()
        {
            var toyotaCars = this._dbContext
                .Cars
                .Select(c => new CarDto()
                {
                    Id = c.CarId,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var serializedToyotaCars = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText(string.Format(ExportPath, ToyotaCarsPath), serializedToyotaCars);
        }

        private void ExportOrderedCustomers()
        {
            var customers = this._dbContext
                .Customers
                .Select(c => new CustomerDto()
                {
                    Id = c.CustomerId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    IsYoungDriver = c.IsYoungDriver,
                    SalesCount = c.Sales.Count
                })
                .OrderBy(c => c.DateOfBirth)
                .ThenBy(c => c.IsYoungDriver)
                .ToArray();

            var serializedCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText(string.Format(ExportPath, OrderedCustomersPath), serializedCustomers);
        }

        public static bool IsValid(object obj)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj), new List<ValidationResult>(), true);
        }
    }
}
