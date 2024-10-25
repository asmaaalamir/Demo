using System.ComponentModel.DataAnnotations;

namespace UpskillingTask.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid non-negative number.")]
        public decimal Price { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid non-negative number.")]
        public int Stock { get; set; }
    }
}
