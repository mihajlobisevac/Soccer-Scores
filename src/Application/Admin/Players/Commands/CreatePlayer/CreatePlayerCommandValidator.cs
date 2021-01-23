using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Admin.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        private readonly IApplicationDbContext context;

        public CreatePlayerCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDate).WithMessage("Invalid date/time format.");

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

        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out _);
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
