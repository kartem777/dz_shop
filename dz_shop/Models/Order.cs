using System.ComponentModel.DataAnnotations;

namespace dz_shop.Models
{
    public class Order
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string DeliveryOption { get; set; }

        public int Price { get; set; }
    }
}
