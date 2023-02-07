using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.DTOs.Order.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        public OrderDtoValidator(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;

            RuleFor(ordr => ordr.OrderName)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(ordr => ordr.State)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
