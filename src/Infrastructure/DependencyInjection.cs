using FluentValidation;
using Hotel.src.Application.Room.AddRoom;

namespace Hotel.src.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddRoomCommandValidator>();
        return services;
    }
}
