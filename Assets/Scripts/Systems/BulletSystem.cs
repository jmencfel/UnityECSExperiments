using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;

public class BulletSystem : ComponentSystem
{
    protected override  void OnUpdate()
    {
        
            Entities.ForEach((ref Translation translation,  ref LifeTimeComponent lifetime) =>
            {
               
                lifetime.Lifetime += Time.DeltaTime;
                if (lifetime.Lifetime>3.0f)
                {
                    lifetime.entityManager.DestroyEntity(lifetime.thisEntity);
                }
            });

    

  
    }

    
}
