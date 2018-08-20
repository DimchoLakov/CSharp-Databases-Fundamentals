using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Prisoner
    {
        public Prisoner()
        {
            this.Mails = new HashSet<Mail>();
            this.PrisonerOfficers = new List<OfficerPrisoner>();
        }

        public int Id { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FullName { get; set; }

        [RegularExpression("^The [A-Z]{1}[a-zA-Z]+$")]
        public string Nickname { get; set; }
        
        [Range(minimum: 18, maximum: 65)]
        public int Age { get; set; }
        
        public DateTime IncarcerationDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }
        public Cell Cell { get; set; }

        public ICollection<Mail> Mails { get; set; }

        public ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
    }
}