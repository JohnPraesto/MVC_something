using System.ComponentModel.DataAnnotations;

namespace CouponAPI.Models.DTOs
{
    public class CouponDTO
    {
        public int ID { get; set; }
        //[Required] // Delete funkar inte om denna är på
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
    }
}
