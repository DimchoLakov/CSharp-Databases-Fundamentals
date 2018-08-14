using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }

        public int CategoryId { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Category name must have min 3 and max 15 chars!")]
        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
