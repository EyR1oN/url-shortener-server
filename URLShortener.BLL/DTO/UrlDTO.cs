namespace URLShortener.BLL.DTO
{
    public class UrlDTO
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
