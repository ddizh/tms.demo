using FluentValidation;

using TMS.Api.Dto.Requests;

namespace TMS.Api.Dto.Validators;

public class UpdateTaskRequestModelValidator : AbstractValidator<UpdateTaskStatusRequestModel>
{
    public UpdateTaskRequestModelValidator()
    {
        RuleFor(x => x.NewStatus)
            .IsInEnum()
            .WithMessage(
                "The status of the task is not valid. Please see the API specification for a list of valid options.");
    }
}