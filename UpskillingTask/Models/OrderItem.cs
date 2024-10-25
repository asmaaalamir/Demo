using System.ComponentModel.DataAnnotations.Schema;

namespace UpskillingTask.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
