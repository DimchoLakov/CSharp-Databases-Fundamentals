using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Export
{
    [XmlType("customer")]
    public class ExportCustomerTotalSalesDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCarsCount { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
