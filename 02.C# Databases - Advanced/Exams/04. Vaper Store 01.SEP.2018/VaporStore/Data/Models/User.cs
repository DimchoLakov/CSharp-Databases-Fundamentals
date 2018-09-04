using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        public User()
        {
            this.Cards = new HashSet<Card>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Username { get; set; }

        [RegularExpression(@"[A-Z]{1}[a-z]+\s{1}[A-Z]{1}[a-z]+")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(minimum: 3, maximum: 103)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
