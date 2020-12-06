using System;
using System.IO.Pipes;

namespace MovieShop.Core.Models.Responses
{
    public class PurchaseDetailResponseModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
    }
}