namespace SummitShop.Core.Specifications
{
    public class ProductSpecificationParameters
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageNumber { get; set; }

        public string? SearchBy { get; set; }
    }
}
