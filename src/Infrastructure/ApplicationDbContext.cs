using Hotel.src.Application.Abstractions;
using Hotel.src.Domain.Abstractions;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.src.Infrastructure;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IPublisher publisher
) : DbContext(options), IUnitOfWork
{
    public DbSet<RoomEntity> Rooms { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync(cancellationToken);

        return result;
    }

    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker
            .Entries<RoomEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var events = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return events;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoomEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .Property(e => e.Id)
                .HasConversion(id => id.Value, value => RoomId.From(value))
                .HasColumnName("Id");

            entity
                .Property(e => e.Number)
                .HasConversion(number => number.Value, value => new RoomNumber(value))
                .HasColumnName("Number");

            entity
                .Property(e => e.MaxOccupancy)
                .HasConversion(
                    occupancy => $"{occupancy.Value}",
                    value => new MaxRoomOccupancy(int.Parse(value))
                )
                .HasColumnName("Occupancy");

            entity
                .Property(e => e.PricePerNight)
                .HasConversion(
                    price => $"{price.Amount}|{(int)price.Currency}",
                    value => new Money(
                        decimal.Parse(value.Split('|')[0]),
                        (Currency)int.Parse(value.Split('|')[1])
                    )
                )
                .HasColumnName("PricePerNight");
        });
    }
}
