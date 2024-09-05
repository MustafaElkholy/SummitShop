using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Entities
{
    public class BasketItem
    {
        // The items from product table to the basket
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? PictureURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? BrandName { get; set; }
        public string? TypeName { get; set; }
    }
}
