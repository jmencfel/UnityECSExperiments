
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Unity.Physics.Systems;
using Unity.Collections;



public class BulletSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        Entities.ForEach((Entity entity, ref LifeTimeComponent bullet, ref PhysicsVelocity velocity) =>
        {
            bullet.Lifetime += Time.DeltaTime;
            if(bullet.Lifetime>3.0f)
            {
                ecb.DestroyEntity(entity);
            }
            if (velocity.Linear.x != bullet.startVelocity.x || velocity.Linear.y != bullet.startVelocity.y)
            {
                ecb.DestroyEntity(entity);
            }

        });
        ecb.Playback(EntityManager);
        ecb.Dispose();

    }
    

}
