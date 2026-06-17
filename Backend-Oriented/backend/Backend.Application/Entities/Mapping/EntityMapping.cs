using Backend.Application.Entities.Dtos;
using Backend.Domain.Entities;

namespace Backend.Application.Entities.Mapping;

/// <summary>Manual domain → DTO mapping.</summary>
public static class EntityMapping
{
    public static EntityDto ToDto(this Entity entity) =>
        new(entity.Id, entity.ScenarioId, entity.Type, entity.TaskForce,
            entity.Name, entity.Latitude, entity.Longitude, entity.UpdatedAt);
}
