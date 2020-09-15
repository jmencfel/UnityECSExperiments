﻿using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct LifeTimeComponent : IComponentData
{
    public float Lifetime;
    public float3 startVelocity;

}
