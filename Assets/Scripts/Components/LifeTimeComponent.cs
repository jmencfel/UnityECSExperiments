using Unity.Entities;
[GenerateAuthoringComponent]
public struct LifeTimeComponent : IComponentData
{
    public float Lifetime;
    public Entity thisEntity;
    public EntityManager entityManager;
}
