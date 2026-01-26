namespace Hotel.src.Domain.Shared;

public readonly record struct Money(decimal Amount, Currency Currency)
{
    public static Money Zero(Currency currency) => new(0m, currency);

    public static Money From(decimal amount, Currency currency)
    {
        if (currency == Currency.None)
            throw new InvalidOperationException("Currency must be specified.");

        var rounded = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        return new Money(rounded, currency);
    }

    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money money, decimal multiplier) =>
        new(
            decimal.Round(money.Amount * multiplier, 2, MidpointRounding.AwayFromZero),
            money.Currency
        );

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Currencies have to be equal.");
    }

    public bool IsZero => Amount == 0m;
}
