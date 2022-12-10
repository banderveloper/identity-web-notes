using System.Reflection;
using AutoMapper;

namespace IdentityWebNotes.Application.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
        => ApplyMappingsFromAssembly(assembly);

    // Method invoked at MAIN, when server starts, applies mapping profiles for each type
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        // Get all classes than implements IMapWith interface
        var iMapWithImplementers = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        // run above each class that implements IMapWith interface
        foreach (var iMapWithImplementer in iMapWithImplementers)
        {
            // locally creates instance of class than inherits from IMapWith
            var instance = Activator.CreateInstance(iMapWithImplementer);

            // gets CUSTOM Mapping method realisation, or DEFAULT realisation described in class-implementer
            var method = iMapWithImplementer.GetMethod("Mapping");

            // invokes method Mapping at given instance at pass AssemblyMappingProfile there
            method?.Invoke(instance, new object?[] { this });
        }
    }
}