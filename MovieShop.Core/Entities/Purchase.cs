using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace MovieShop.Core.Entities
{
    [Table("Purchase")]
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}