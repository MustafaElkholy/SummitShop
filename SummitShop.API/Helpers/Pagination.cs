namespace SummitShop.API.Helpers
{
    public class Pagination<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; } = new List<T>();
    }
}
