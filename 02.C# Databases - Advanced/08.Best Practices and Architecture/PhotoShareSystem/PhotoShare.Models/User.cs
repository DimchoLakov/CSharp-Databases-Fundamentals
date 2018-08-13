namespace PhotoShare.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.FriendsAdded = new HashSet<Friendship>();
            this.AlbumRoles = new HashSet<AlbumRole>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int? ProfilePictureId { get; set; }
        public virtual Picture ProfilePicture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public int? BornTownId { get; set; }
        public virtual Town BornTown { get; set; }

        public int? CurrentTownId { get; set; }
        public virtual Town CurrentTown { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        public int? Age { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual ICollection<Friendship> FriendsAdded { get; set; }

        public virtual ICollection<AlbumRole> AlbumRoles { get; set; }
    }
}
