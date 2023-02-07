using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.DTOs.SubElement.Validators
{
    public class SubElementDtoValidator : AbstractValidator<SubElementDto>
    {
        private readonly ISubElementRepository _subElementRepository;
        public SubElementDtoValidator(ISubElementRepository subELementRepository)
        {
            this._subElementRepository = subELementRepository;

            RuleFor(subEL => subEL.Element)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be larger than {ComparisonValue}");

            RuleFor(ordr => ordr.Type)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(subEL => subEL.Height)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be larger than {ComparisonValue}");

            RuleFor(subEL => subEL.Width)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be larger than {ComparisonValue}");
        }
    }
}
