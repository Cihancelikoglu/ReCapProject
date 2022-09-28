using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string CustomerName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TotalDay { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
