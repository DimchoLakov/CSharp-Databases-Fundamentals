using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
	public class Game
    {
        public Game()
        {
            this.Purchases = new List<Purchase>();
            this.GameTags = new HashSet<GameTag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int DeveloperId { get; set; }
        [Required]
        public Developer Developer { get; set; }

        [Required]
        public int GenreId { get; set; }
        [Required]
        public Genre Genre { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        [Required, MinLength(1)]
        public ICollection<GameTag> GameTags { get; set; }
	}
}
