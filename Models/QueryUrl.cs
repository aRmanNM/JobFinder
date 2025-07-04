namespace JobFinder.Models
{
    public class QueryUrl
    {
        public string? SearchString { get; set; }
        public string? Location { get; set; }
        public string? Topic { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}