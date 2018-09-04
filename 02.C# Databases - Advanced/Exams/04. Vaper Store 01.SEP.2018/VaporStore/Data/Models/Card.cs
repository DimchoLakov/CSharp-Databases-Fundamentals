using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"\d{4}\s{1}\d{4}\s{1}\d{4}\s{1}\d{4}")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"\d{3}")]
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
