using Hotel.src.Domain.Customer.Values;

namespace Hotel.src.Domain.Customer;

public sealed class CustomerEntity(
    CustomerId? id,
    FirstName firstName,
    LastName lastName,
    Email email
) : Entity<CustomerId>(id ?? CustomerId.Generate())
{
    public FirstName FirstName { get; } = firstName;
    public LastName LastName { get; } = lastName;
    public Email Email { get; } = email;
}
