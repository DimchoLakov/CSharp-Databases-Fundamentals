using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Deserializer
    {
        private const string FailureMsg = "Invalid Data";

        private const string SuccessImportDepartmentCells = "Imported {0} with {1} cells";
        private const string SuccessImportPrisonerMails = "Imported {0} {1} years old";
        private const string SuccessImportOfficersPrisoners = "Imported {0} ({1} prisoners)";

        private const string DateFormat = "dd/MM/yyyy";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentDtos = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            var sb = new StringBuilder();

            var departments = new List<Department>();

            foreach (var departmentDto in departmentDtos)
            {
                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var areAllCellsValid = departmentDto.Cells.All(IsValid);

                if (!areAllCellsValid)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var department = Mapper.Map<Department>(departmentDto);

                departments.Add(department);

                sb.AppendLine(string.Format(SuccessImportDepartmentCells, department.Name, department.Cells.Count));
            }

            context
                .Departments
                .AddRange(departments);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                DateFormatString = DateFormat, Culture = CultureInfo.InvariantCulture,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var deserializedPrisonerMails = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString, jsonSerializerSettings);

            var sb = new StringBuilder();

            var prisoners = new List<Prisoner>();

            foreach (var prisonerDto in deserializedPrisonerMails)
            {
                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var areMailsValid = prisonerDto.Mails.All(IsValid);

                if (!areMailsValid)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var prisoner = Mapper.Map<Prisoner>(prisonerDto);

                prisoners.Add(prisoner);

                sb.AppendLine(string.Format(SuccessImportPrisonerMails, prisoner.FullName, prisoner.Age));
            }

            context
                .Prisoners
                .AddRange(prisoners);

            context
                .SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));

            var deserializedOfficerPrisoners = (OfficerDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var officers = new List<Officer>();

            foreach (var officerDto in deserializedOfficerPrisoners)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var validPrisoners = new List<OfficerPrisoner>();

                var hasInvalidPrisoners = false;
                
                foreach (var prisonerDto in officerDto.Prisoners)
                {
                    if (!IsValid(prisonerDto))
                    {
                        hasInvalidPrisoners = true;
                        break;
                    }

                    var prisoner = new OfficerPrisoner()
                    {
                        PrisonerId = prisonerDto.Id
                    };

                    validPrisoners.Add(prisoner);
                }

                if (hasInvalidPrisoners)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var isPositionValid = Enum.TryParse(officerDto.Position, out Position position);
                var isWeaponValid = Enum.TryParse(officerDto.Weapon, out Weapon weapon);

                if (!isPositionValid || !isWeaponValid)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var officer = new Officer()
                {
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = officerDto.DepartmentId,
                    OfficerPrisoners = validPrisoners
                };

                officers.Add(officer);

                sb.AppendLine(string.Format(SuccessImportOfficersPrisoners, officer.FullName,
                    officer.OfficerPrisoners.Count));
            }

            context
                .Officers
                .AddRange(officers);

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