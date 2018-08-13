namespace PhotoShare.Models
{
    public class AlbumTag 
    {
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
