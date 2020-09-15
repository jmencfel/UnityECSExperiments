
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Unity.Physics.Systems;
using Unity.Collections;



public class AsteroidRespawnSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        Entities.ForEach((Entity entity, ref AsteroidComponent asteroid, ref PhysicsVelocity velocity, ref Translation translation) =>
        {

            if (velocity.Linear.x != asteroid.startVelocity.x || velocity.Linear.y != asteroid.startVelocity.y)
            {
                int signX = UnityEngine.Random.Range(0,1);
                float newX = (signX==1) ? UnityEngine.Random.Range(10, 100) : UnityEngine.Random.Range(-100, -10);
                int signY = UnityEngine.Random.Range(0, 1);
                float newY = (signX == 1) ? UnityEngine.Random.Range(10, 100) : UnityEngine.Random.Range(-100, -10);

                float newVelocityX = UnityEngine.Random.Range(-1.0f, 1.0f);
                float newVelocityY = UnityEngine.Random.Range(-1.0f, 1.0f);
                ecb.SetComponent(entity, new Translation
                {
                    Value = new float3(newX, newY, 0)
                });
                ecb.SetComponent(entity, new AsteroidComponent
                {
                    startVelocity = new float3(newVelocityX, newVelocityY, 0)
                });
                ecb.SetComponent(entity, new PhysicsVelocity
                {
                    Linear = new float3(newVelocityX, newVelocityY, 0)
                });

            }

        });
        ecb.Playback(EntityManager);
        ecb.Dispose();

    }


}
