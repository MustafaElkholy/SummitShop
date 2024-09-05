﻿namespace SummitShop.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; } // one to one
    }
}