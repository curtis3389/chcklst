namespace Chcklst.Domain.SharedKernel;

public interface IRepository<EntityType, IdType, IdValueType>
    where EntityType : AbstractEntity<IdType, IdValueType>
    where IdType: AbstractEntityId<IdValueType>
{
    EntityType Create(EntityType entityToCreate);
    Task<EntityType> CreateAsync(EntityType entityToCreate, CancellationToken cancellationToken = default);
    EntityType Delete(EntityType entityToDelete);
    Task<EntityType> DeleteAsync(EntityType entityToDelete, CancellationToken cancellationToken = default);
    IEnumerable<EntityType> List(Func<EntityType, bool>? predicate = null);
    Task<IEnumerable<EntityType>> ListAsync(Func<EntityType, bool>? predicate = null, CancellationToken cancellationToken = default);
    EntityType Read(IdType id);
    Task<EntityType> ReadAsync(IdType id, CancellationToken cancellationToken = default);
    EntityType Update(EntityType entityToUpdate);
    Task<EntityType> UpdateAsync(EntityType entityToUpdate, CancellationToken cancellationToken = default);
}
