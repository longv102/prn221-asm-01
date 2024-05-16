namespace BO.Dtos
{
    public class NewsArticleResponse
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }
        
        public string? CategoryName { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
