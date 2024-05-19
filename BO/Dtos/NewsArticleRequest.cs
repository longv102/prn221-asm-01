namespace BO.Dtos
{
    public class NewsArticleRequest
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public string? NewsContent { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        public short CategoryId { get; set; }

        //public string CategoryName { get; set; } = null!;

        public short CreatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
