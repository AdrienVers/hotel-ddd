using Hotel.src.Application;
using Hotel.src.Domain.Room;
using Hotel.src.Domain.Room.Values;
using Hotel.src.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel.src.Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options),
        IUnitOfWork
{
    public DbSet<RoomEntity> Rooms { get; set; }

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
