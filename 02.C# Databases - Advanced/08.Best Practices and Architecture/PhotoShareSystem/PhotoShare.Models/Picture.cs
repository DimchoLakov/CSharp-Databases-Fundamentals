namespace PhotoShare.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public int UserProfileId { get; set; }
        public virtual User UserProfile { get; set; }
    }
}
