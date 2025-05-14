using FluentValidation;

using TMS.Api.Dto.Requests;

namespace TMS.Api.Dto.Validators;

public sealed class CreateTaskRequestModelValidator: AbstractValidator<CreateTaskRequestModel>
{
    public CreateTaskRequestModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters long");
        
        RuleFor(x => x.Description)
            .Length(10, 200).WithMessage("Description must be between 10 and 200 characters long")
            .When(x => x.Description is not null);
    }
}