using AutoMapper;

namespace IdentityWebNotes.Application.Common.Mappings;

// Interface created for auto mapping, each DTO inherits from this interface
// Method ApplyMappingsFromAssembly from AssemblyMappingProfile
// using reflections finds all classes than inherits this interface and call Mapping method in them

// this makes it unnecessary to write a large list of mapping profiles.
// Each dto will describe its own mapping config, and you won't have to look for it.
public interface IMapWith<T>
{
    // Method which calls in every instance inherited from IMapWith (calls by assemblyMappingProfile)
    // This is a DEFAULT implementation, but it can be custom inside class-implementer
    void Mapping(Profile profile)
    {
        // create DEFAULT map config for _mapper.Map
        profile.CreateMap(typeof(T), GetType());
    }
}