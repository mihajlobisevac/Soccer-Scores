using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Common.Shared;
using System;
using System.Linq;

namespace SoccerScores.Application.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdatePlayerCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Players.Any(p => p.Id == id)).WithMessage("Player does not exist.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DateOfBirth)
                .Must(CustomValidators.BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.Position)
                .Must(type => Enum.IsDefined(typeof(Position), type)).WithMessage("Position type is invalid.");

            RuleFor(x => x.Foot)
                .Must(type => Enum.IsDefined(typeof(Foot), type)).WithMessage("Foot type is invalid.");

            RuleFor(x => x.NationalityId)
                .NotEmpty()
                .Must(id => context.Countries.Any(c => c.Id == id)).WithMessage("Nationality (country) does not exist.");

            RuleFor(x => x.PlaceOfBirthId)
                .NotEmpty()
                .Must(id => context.Cities.Any(c => c.Id == id)).WithMessage("Place of birth (city) does not exist.");

            RuleFor(x => x.ClubId)
                .Must(id => BeAValidClubIfNotNull(id)).WithMessage("Club does not exist.");
        }

        private bool BeAValidClubIfNotNull(int? id)
        {
            if (id == null || id == 0)
            {
                return true;
            }

            return context.Clubs.Any(c => c.Id == id);
        }
    }
}
