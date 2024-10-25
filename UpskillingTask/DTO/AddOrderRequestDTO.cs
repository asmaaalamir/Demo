using System.ComponentModel.DataAnnotations.Schema;

namespace UpskillingTask.Models
{
    public class AddOrderRequestDTO
    {
        public long CustomerId { get; set; }
        public List<AddProductsDTO> Products { get; set; }

    }
    public class AddProductsDTO
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
