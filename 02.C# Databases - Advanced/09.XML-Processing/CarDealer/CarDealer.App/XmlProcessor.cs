using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.App.DTOs.Export;
using CarDealer.App.DTOs.Import;
using CarDealer.Data;
using CarDealer.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace CarDealer.App
{
    public class XmlProcessor
    {
        private const string ImportPath = "./../../../Resources/{0}";
        private const string SuppliersPath = "suppliers.xml";
        private const string PartsPath = "parts.xml";
        private const string CarsPath = "cars.xml";
        private const string CustomersPath = "customers.xml";

        private const string ExportPath = "./../../../XmlExport/{0}";
        private const string CarsWithDistancePath = "cars-with-distance.xml";
        private const string FerrariCarsPath = "ferrari-cars.xml";
        private const string LocalSuppliersPath = "local-suppliers.xml";
        private const string CarsWithTheirPartsPath = "cars-with-their-parts.xml";
        private const string CustomerTotalSalesPath = "customer-total-sales.xml";
        private const string SalesDiscountPath = "sales-discount.xml";

        private readonly CarDealerDbContext _dbContext;

        public XmlProcessor()
        {
            this._dbContext = new CarDealerDbContext();
        }

        public void ImportData()
        {
            MapperInitializer.InitializeMapper();
            this.ImprortSuppliers();
            this.ImportParts();
            this.ImportCars();
            this.ImportCustomers();
            this.ImportSaleRecords();
        }

        public void ExportData()
        {
            this.ExportCarsWithDistance();
            this.ExportCarsWithMakeFerrari();
            this.ExportLocalSuppliers();
            this.ExportCarsWithTheirParts();
            this.ExportTotalSalesByCustomer();
            this.ExportSalesDiscount();
        }

        private void ExportSalesDiscount()
        {
            var sales = this._dbContext
                .Sales
                .Select(s => new ExportSaleDiscountDto()
                {
                    Car = new ExportCarAttributesDto()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car
                        .CarParts
                        .Select(cp => cp.Part.Price)
                        .DefaultIfEmpty(0)
                        .Sum(),
                    PriceWithDiscount =  (s.Car
                                            .CarParts
                                            .Select(cp => cp.Part.Price)
                                            .DefaultIfEmpty(0)
                                            .Sum() / (s.Discount + 1)).ToString("f2")
                })
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportSaleDiscountDto[]), new XmlRootAttribute("sales"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), sales, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, SalesDiscountPath), sb.ToString().Trim());
        }

        private void ExportTotalSalesByCustomer()
        {
            var customers = this._dbContext
                .Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new ExportCustomerTotalSalesDto()
                {
                    FullName = c.Name,
                    BoughtCarsCount = c.Sales.Count,
                    SpentMoney = c.Sales
                        .Select(s => s.Car.CarParts.Sum(cp => cp.Part.Price) * (s.Discount + 1))
                        .DefaultIfEmpty(0)
                        .Sum()
                })
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportCustomerTotalSalesDto[]), new XmlRootAttribute("customers"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), customers, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, CustomerTotalSalesPath), sb.ToString().Trim());
        }

        private void ExportCarsWithTheirParts()
        {
            var carsWithParts = this._dbContext
                .Cars
                .Select(c => new ExportCarWithPartsDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.CarParts.Select(cp => new ExportPartDto()
                    {
                        Name = cp.Part.Name,
                        Price = cp.Part.Price
                    })
                        .ToArray()
                })
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportCarWithPartsDto[]), new XmlRootAttribute("cars"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), carsWithParts, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, CarsWithTheirPartsPath), sb.ToString().Trim());
        }

        private void ExportLocalSuppliers()
        {
            var localSuppliers = this._dbContext
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportLocalSupplierDto()
                {
                    SupplierId = s.SupplierId,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportLocalSupplierDto[]), new XmlRootAttribute("suppliers"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), localSuppliers, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, LocalSuppliersPath), sb.ToString().Trim());
        }

        private void ExportCarsWithMakeFerrari()
        {
            var ferrariCars = this._dbContext
                .Cars
                .Where(c => c.Make.ToLower().Equals("ferrari"))
                .Select(c => new ExportCarFerrariDto()
                {
                    CarId = c.CarId,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportCarFerrariDto[]), new XmlRootAttribute("cars"));

            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(sb), ferrariCars, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, FerrariCarsPath), sb.ToString().Trim());
        }

        private void ExportCarsWithDistance()
        {
            var cars = this._dbContext
                .Cars
                .Where(c => c.TravelledDistance > 2_000_000)
                .Select(c => new ExportCarDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Make)
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportCarDto[]), new XmlRootAttribute("cars"));

            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);

            File.WriteAllText(string.Format(ExportPath, CarsWithDistancePath), sb.ToString().Trim());
        }

        private void ImportSaleRecords()
        {
            var sales = new List<Sale>();

            var random = new Random();

            var discountsArr = new decimal[] { 0m, 0.5m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };
            var carsCount = this._dbContext
                .Cars
                .Count();
            var customersCount = this._dbContext
                .Customers
                .Count();

            for (int i = 0; i < 5000; i++)
            {
                var sale = new Sale()
                {
                    CarId = random.Next(1, carsCount + 1),
                    CustomerId = random.Next(1, customersCount + 1),
                    Discount = discountsArr[random.Next(0, discountsArr.Length)]
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
            var customersXmlString = File.ReadAllText(string.Format(ImportPath, CustomersPath));

            var xmlSerializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            var deserialzedCustomers = (CustomerDto[])xmlSerializer.Deserialize(new StringReader(customersXmlString));

            var customers = new List<Customer>();

            foreach (var customerDto in deserialzedCustomers)
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
            var carsXmlString = File.ReadAllText(string.Format(ImportPath, CarsPath));

            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
            var deserializedCars = (CarDto[])serializer.Deserialize(new StringReader(carsXmlString));

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

            this._dbContext
                .Cars
                .AddRange(cars);

            this._dbContext
                .SaveChanges();

            var partsCount = this._dbContext
                .Parts
                .Count();

            var carParts = GenerateCarParts(partsCount);

            this._dbContext
                .CarParts
                .AddRange(carParts);

            this._dbContext
                .SaveChanges();
        }

        private CarPart[] GenerateCarParts(int partsCount)
        {
            var random = new Random();

            var minParts = 10;
            var maxParts = 20;

            var count = random.Next(minParts, maxParts + 1);

            var carParts = new List<CarPart>();

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

                    var carPart = new CarPart()
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
            var xmlPartsString = File.ReadAllText(string.Format(ImportPath, PartsPath));

            var xmlSerializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
            var deserializedParts = (PartDto[])xmlSerializer.Deserialize(new StringReader(xmlPartsString));

            var parts = new List<Part>();

            foreach (var partDto in deserializedParts)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                var part = Mapper.Map<Part>(partDto);

                var suppliersCount = this._dbContext
                    .Suppliers
                    .Count();

                part.SupplierId = new Random().Next(1, suppliersCount + 1);

                parts.Add(part);
            }

            this._dbContext
                .Parts
                .AddRange(parts);

            this._dbContext
                .SaveChanges();
        }

        private void ImprortSuppliers()
        {
            var xmlSuppliersString = File.ReadAllText(string.Format(ImportPath, SuppliersPath));

            var xmlSerializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            var deserializedSuppliers = (SupplierDto[])xmlSerializer.Deserialize(new StringReader(xmlSuppliersString));

            var suppliers = new List<Supplier>();

            foreach (var supplierDto in deserializedSuppliers)
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

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
