using System.ComponentModel.DataAnnotations;

namespace CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
