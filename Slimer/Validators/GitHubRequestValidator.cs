﻿using FluentValidation;
using Slimer.Domain.Contracts.GitHub;

namespace Slimer.Validators
{
    public class GitHubRequestValidator : AbstractValidator<GitHubRequest>
    {
        public GitHubRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(1, 255);
            RuleFor(x => x.Body).NotEmpty().Length(1, 255);
            RuleFor(x => x.Labels).NotNull().NotEmpty();
            RuleForEach(x => x.Labels)
                .NotNull()
                .NotEmpty()
                .Must(x => string.Equals(x, "BUG", StringComparison.OrdinalIgnoreCase));
        }
    }
}
