using FluentValidation;
using Forum.Api.Models.Dto;

namespace Forum.Api.Validation;

public class CreatorRequestDtoValidator : AbstractValidator<CreatorRequestDto>
{
    public CreatorRequestDtoValidator()
    {
        RuleFor(creator => creator.Login).Length(2, 52);
    }
}