using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;

public class ShooterSystem : ComponentSystem
{
    
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PlayerDataComponent playerData, ref Translation translation, ref Rotation rotation) =>
        {
            playerData.BulletTimer += Time.DeltaTime;
            if(playerData.BulletTimer>0.5f)
            {
                playerData.BulletTimer = 0;
               var bullet =  playerData.entityManager.Instantiate(playerData.BulletPrefab);

                var direction = math.mul(rotation.Value, new float3(0f, 1f, 0f));
                playerData.entityManager.SetComponentData(bullet, new Translation
                {
                    Value = translation.Value
                });
                playerData.entityManager.SetComponentData(bullet, new PhysicsVelocity
                {
                    Linear = direction * 15

                });

                playerData.entityManager.SetComponentData(bullet, new Rotation
                {
                    Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(90)))

                });
                playerData.entityManager.SetComponentData(bullet, new LifeTimeComponent
                {
                    Lifetime = 0,
                    thisEntity = bullet,
                    entityManager = playerData.entityManager
                });

            }
        });
    }
}
