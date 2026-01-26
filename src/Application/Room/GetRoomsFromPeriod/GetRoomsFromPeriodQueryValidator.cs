using FluentValidation;
using Hotel.src.Domain.Shared;

namespace Hotel.src.Application.Room.GetRoomsFromPeriod;

public sealed class GetRoomsFromPeriodQueryValidator : AbstractValidator<GetRoomsFromPeriodQuery>
{
    public GetRoomsFromPeriodQueryValidator()
    {
        RuleFor(query => query.Start).NotNull().WithMessage("Start date is required.");

        RuleFor(query => query.End).NotNull().WithMessage("End date is required.");

        RuleFor(query => query)
            .Must(query =>
                query.Start.HasValue
                && query.End.HasValue
                && Period.IsValid(query.Start.Value, query.End.Value)
            )
            .WithMessage("End date must be after start date.");
    }
}
