using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Vet
    {
        public Vet()
        {
            this.Procedures = new HashSet<Procedure>();
        }

        public int Id { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Profession { get; set; }

        [Range(minimum: 22, maximum: 65)]
        public int Age { get; set; }

        [RegularExpression(@"(\+359\d{9})|(0\d{9})")]
        public string PhoneNumber { get; set; }

        public ICollection<Procedure> Procedures { get; set; }
    }
}
