namespace Hotel.src.Domain.Review.Values;

public readonly record struct ReviewId : IEntityId<ReviewId>
{
    public Guid Value { get; }

    // private constructor
    private ReviewId(Guid value) => Value = value;

    // create a new unique id
    public static ReviewId Generate() => new(Guid.NewGuid());

    // create from existing guid
    public static ReviewId From(Guid value) =>
        value == Guid.Empty
            ? throw new ArgumentException("ReviewId cannot be empty.", nameof(value))
            : new ReviewId(value);
}
