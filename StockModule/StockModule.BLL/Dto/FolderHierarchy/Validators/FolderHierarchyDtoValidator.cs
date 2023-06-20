using FluentValidation;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Dto.FolderHierarchy.Validators
{
    public class FolderHierarchyDtoValidator : AbstractValidator<FolderHierarchyDto>
    {
        private readonly IFolderHierarchyService _folderHierarchyService;
        public FolderHierarchyDtoValidator(IFolderHierarchyService folderHierarchyService)
        {
            this._folderHierarchyService = folderHierarchyService;

            RuleFor(fldrhrchy => fldrhrchy.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(ordr => ordr.Icon)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
