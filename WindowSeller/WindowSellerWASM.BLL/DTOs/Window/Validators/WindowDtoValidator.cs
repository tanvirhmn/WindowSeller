using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.DTOs.Window.Validators
{
    public class WindowDtoValidator: AbstractValidator<WindowDto>
    {
        private readonly IWindowRepository _windowRepository;
        public WindowDtoValidator(IWindowRepository windowRepository)
        {
            this._windowRepository = windowRepository;

            RuleFor(wndw => wndw.WindowName)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(wndw => wndw.QuantityOfWindows)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be larger than {ComparisonValue}");

            RuleFor(wndw => wndw.TotalSubELements)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be larger than {ComparisonValue}");

        }
    }
}
