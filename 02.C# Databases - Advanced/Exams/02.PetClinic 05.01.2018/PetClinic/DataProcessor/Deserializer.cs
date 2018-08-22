using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.DTOs.Import;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Error: Invalid data.";
        private const string SuccessfullyImportedAnimalAid = "Record {0} successfully imported.";
        private const string SuccessfullyImportedAnimalPassport =
            "Record {0} Passport №: {1} successfully imported.";
        private const string SuccessfullyImportedVet = "Record {0} successfully imported.";
        private const string SuccessfullyImportedProcedure = "Record successfully imported.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            var sb = new StringBuilder();

            var animalAids = new List<AnimalAid>();

            foreach (var animalAidDto in deserializedAnimalAids)
            {
                if (!IsValid(animalAidDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var animalAid = Mapper.Map<AnimalAid>(animalAidDto);

                if (animalAids.Any(x => x.Name == animalAid.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                animalAids.Add(animalAid);
                sb.AppendLine(string.Format(SuccessfullyImportedAnimalAid, animalAid.Name));
            }

            context
                .AnimalAids
                .AddRange(animalAids);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                DateFormatString = "dd-MM-yyyy"
            };
            var deserializedAnimals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString, jsonSettings);

            var sb = new StringBuilder();

            var animals = new List<Animal>();
            
            foreach (var animalDto in deserializedAnimals)
            {
                var isExistingPassport = animals.Any(p => p.Passport.SerialNumber == animalDto.Passport.SerialNumber);
                
                if (!IsValid(animalDto) || !IsValid(animalDto.Passport) || isExistingPassport)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                var animal = Mapper.Map<Animal>(animalDto);

                animals.Add(animal);

                sb.AppendLine(string.Format(SuccessfullyImportedAnimalPassport, animal.Name,
                    animal.Passport.SerialNumber));
            }

            context
                .Animals
                .AddRange(animals);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets =(VetDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var vets = new List<Vet>();

            foreach (var vetDto in deserializedVets)
            {
                var hasNumber = vets.Any(v => v.PhoneNumber == vetDto.PhoneNumber);

                if (!IsValid(vetDto) || hasNumber)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var vet = Mapper.Map<Vet>(vetDto);

                vets.Add(vet);

                sb.AppendLine(string.Format(SuccessfullyImportedVet, vet.Name));
            }

            context
                .Vets
                .AddRange(vets);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedProcedures = (ProcedureDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var procedures = new List<Procedure>();

            foreach (var procedureDto in deserializedProcedures)
            {
                var vet = context.Vets.FirstOrDefault(v => v.Name == procedureDto.Vet);
                var animal = context.Animals.FirstOrDefault(a => a.PassportSerialNumber == procedureDto.SerialNumber);
                if (vet == null || animal == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                var proceduresAnimalAids = new List<ProcedureAnimalAid>();

                var hasInvalidAnimalAid = false;

                foreach (var animalAidDto in procedureDto.AnimalAids)
                {
                    var animalAid = context.AnimalAids.FirstOrDefault(a => a.Name == animalAidDto.Name);
                    var animalAidExists = proceduresAnimalAids.Any(paa => paa.AnimalAid.Name == animalAidDto.Name);

                    if (animalAid == null || animalAidExists)
                    {
                        hasInvalidAnimalAid = true;
                        break;
                    }

                    var procedureAnimalAid = new ProcedureAnimalAid()
                    {
                        AnimalAid = animalAid
                    };

                    proceduresAnimalAids.Add(procedureAnimalAid);
                }

                if (hasInvalidAnimalAid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var procedure = new Procedure()
                {
                    Animal = animal,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Vet = vet,
                    ProcedureAnimalAids = proceduresAnimalAids
                };

                procedures.Add(procedure);

                sb.AppendLine(SuccessfullyImportedProcedure);
            }

            context
                .Procedures
                .AddRange(procedures);

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
