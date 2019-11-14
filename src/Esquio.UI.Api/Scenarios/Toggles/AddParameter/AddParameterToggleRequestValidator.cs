﻿using FluentValidation;

namespace Esquio.UI.Api.Features.Toggles.AddParameter
{
    public class DeleteToggleRequestValidator
        : AbstractValidator<AddParameterToggleRequest>
    {
        public DeleteToggleRequestValidator()
        {
            RuleFor(x => x.ProductName)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(200)
               .Matches(ApiConstants.Constraints.NamesRegularExpression);

            RuleFor(x => x.FeatureName)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(200)
               .Matches(ApiConstants.Constraints.NamesRegularExpression);

            this.RuleFor(pr => pr.ToggleType)
                .NotEmpty();

            this.RuleFor(pr => pr.Name)
                .NotEmpty();

            this.RuleFor(rf => rf.Value)
                .NotEmpty();
        }
    }
}
