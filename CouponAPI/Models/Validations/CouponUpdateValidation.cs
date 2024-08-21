using CouponAPI.Models.DTOs;
using FluentValidation;

namespace CouponAPI.Models.Validations
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
    {
        public CouponUpdateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.ID).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }
    }
}
