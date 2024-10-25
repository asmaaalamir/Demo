using System.ComponentModel.DataAnnotations.Schema;

namespace UpskillingTask.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
