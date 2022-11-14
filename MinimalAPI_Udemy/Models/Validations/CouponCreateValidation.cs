using FluentValidation;
using MinimalAPI_Udemy.Models.DTOs;

namespace MinimalAPI_Udemy.Models.Validations
{
    public class CouponCreateValidation:AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }
    }
}
