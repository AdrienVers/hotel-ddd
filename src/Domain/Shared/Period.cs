namespace Hotel.src.Domain.Shared;

public readonly record struct Period
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }

    public Period(DateOnly start, DateOnly end)
    {
        if (!IsValid(start, end))
        {
            throw new ArgumentException("La date de début doit être antérieure à la date de fin.");
        }

        Start = start;
        End = end;
    }

    public static bool IsValid(DateOnly start, DateOnly end) => start < end;

    public int Nights =>
        (int)(End.ToDateTime(TimeOnly.MinValue) - Start.ToDateTime(TimeOnly.MinValue)).TotalDays;

    public bool Overlaps(Period period) => Start < period.End && End > period.Start;
}
