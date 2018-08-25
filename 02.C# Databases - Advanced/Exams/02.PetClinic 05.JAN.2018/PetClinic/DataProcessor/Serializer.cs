using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.DTOs.Export;
using Formatting = Newtonsoft.Json.Formatting;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context
                .Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(ao => new AnimalByOwnerPhoneNumberDto()
                {
                    SerialNumber = ao.PassportSerialNumber,
                    Age = ao.Age,
                    AnimalName = ao.Name,
                    OwnerName = ao.Passport.OwnerName,
                    RegisteredOn = ao.Passport.RegistrationDate
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToArray();

            var jsonSettings = new JsonSerializerSettings()
            {
                DateFormatString = "dd-MM-yyyy",
                Culture = CultureInfo.InvariantCulture
            };

            var serializedAnimals = JsonConvert.SerializeObject(animals, Formatting.Indented, jsonSettings);

            return serializedAnimals;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var sb = new StringBuilder();

            var procedures = context
                .Procedures
                .OrderBy(p => p.DateTime)
                .ThenBy(p => p.Animal.PassportSerialNumber)
                .Select(p => new ExportProcedureDto()
                {
                    PassportSerialNumber = p.Animal.PassportSerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids
                        .Select(aa => new ExportProcedureAnimalAidDto()
                        {
                            Name = aa.AnimalAid.Name,
                            Price = aa.AnimalAid.Price
                        })
                        .ToArray(),
                    TotalPrice = p.ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price)
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportProcedureDto[]), new XmlRootAttribute("Procedures"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), procedures, xmlNamespaces);

            return sb.ToString().Trim();
        }
    }
}
