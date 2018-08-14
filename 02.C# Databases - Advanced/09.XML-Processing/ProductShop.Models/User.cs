using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> ProductsBought { get; set; }
        public virtual ICollection<Product> ProductsSold { get; set; }
    }
}
