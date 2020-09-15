
using Unity.Entities;


[GenerateAuthoringComponent]
public struct PlayerDataComponent : IComponentData
{
    public float movementSpeed;
    public float angularSpeed;
    public Entity BulletPrefab;
    public float BulletTimer;
    public EntityManager entityManager;
}
