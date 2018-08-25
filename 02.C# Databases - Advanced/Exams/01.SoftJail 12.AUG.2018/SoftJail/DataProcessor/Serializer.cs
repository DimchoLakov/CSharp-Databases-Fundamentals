using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SoftJail.DataProcessor.ExportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .Where(p => ids.Any(id => p.Id == id))
                .Select(p => new ExpPrisonerOfficerDto()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new ExpOfficerDto()
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers
                        .Select(po => po.Officer.Salary)
                        .DefaultIfEmpty(0)
                        .Sum()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();

            var serializedPrisoners = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return serializedPrisoners;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var inmatesNames = prisonersNames.Split(",").ToArray();

            var prisoners = context
                .Prisoners
                .Where(p => inmatesNames.Any(i => i == p.FullName))
                .Select(p => new PrisonerInboxDto()
                {
                    Id = p.Id,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    Name = p.FullName,
                    EncryptedMessages = new EncryptedMessagesDto()
                    {
                        Messages = p.Mails
                            .Select(m => new MessageDto()
                            {
                                Description = ReverseDescription(m.Description)
                            })
                            .ToArray()
                    }
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(PrisonerInboxDto[]), new XmlRootAttribute("Prisoners"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), prisoners, xmlNamespaces);

            return sb.ToString().Trim();
        }

        private static string ReverseDescription(string description)
        {
            var sb = new StringBuilder();

            for (int i = description.Length - 1; i >= 0; i--)
            {
                sb.Append(description[i]);
            }

            return sb.ToString().Trim();
        }
    }
}