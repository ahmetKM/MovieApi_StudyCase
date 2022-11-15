using FluentValidation;
using MovieApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Validaitons
{
    public class MovieDtoValidator:AbstractValidator<MovieDto>
    {
        public MovieDtoValidator() 
        {
            RuleFor(x => x.Title).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Duration).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
            RuleFor(x => x.Genre).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
