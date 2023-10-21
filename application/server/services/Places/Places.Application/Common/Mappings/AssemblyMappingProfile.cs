using System.Reflection;
using AutoMapper;
using Places.Application.Dtos;
using Places.Application.Likes.CreatePlaceLike;
using Places.Application.Places.Commands.CreateManyPlaces;
using Places.Application.Places.Queries.GetPlaceByCoordinate;
using Places.Domain;

namespace Places.Application.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
    {
        ApplyMappingFromAssembly(assembly);
        CreateMap<PlacesApiResponse, CreateManyPlacesCommand>();
        CreateMap<Place, PlaceByCoordinatesViewModel>();
        CreateMap<PlaceLike, PlaceLikeViewModel>();
        CreateMap<CreatePlaceLikeCommand, PlaceLike>();
    }

    private void ApplyMappingFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
