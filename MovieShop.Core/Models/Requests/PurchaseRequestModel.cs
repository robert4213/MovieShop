using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MovieShop.Core.Models.Requests
{
    public class PurchaseRequestModel
    {
        [Required]
        public int UserId { get; set; }
        public Guid? PurchaseNumber { get; set; }
        [Required]
        public Decimal TotalPrice { get; set; }
        public DateTime? PurchaseDateTime { get; set; }
        [Required]
        public int MovieId { get; set; }
        
    }
}