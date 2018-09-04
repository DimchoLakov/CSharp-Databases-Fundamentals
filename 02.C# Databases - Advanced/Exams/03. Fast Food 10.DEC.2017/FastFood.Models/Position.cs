using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Position
    {
        public Position()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; } // Make as UNIQUE with Fluent API

        public ICollection<Employee> Employees { get; set; }
    }
}
