namespace PhotoShare.Models
{
    using Models.Enums;

    public class AlbumRole
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public virtual Role Role { get; set; }
    }
}
