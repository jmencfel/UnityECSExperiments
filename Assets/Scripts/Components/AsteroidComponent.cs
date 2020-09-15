using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct AsteroidComponent : IComponentData
{
    public float3 startVelocity;
}
