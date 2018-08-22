using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Animal
    {
        public Animal()
        {
            this.Procedures = new HashSet<Procedure>();
        }

        public int Id { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Type { get; set; }

        [Range(1, 500)]
        public int Age { get; set; }

        public string PassportSerialNumber { get; set; }
        public Passport Passport { get; set; }

        public ICollection<Procedure> Procedures { get; set; }
    }
}
