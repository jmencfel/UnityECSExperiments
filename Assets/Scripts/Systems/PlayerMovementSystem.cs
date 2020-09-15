using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class PlayerMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PlayerDataComponent playerData, ref Translation translation, ref Rotation rotation) =>
        {
            if (Input.GetKey(KeyCode.W))
            {
                var direction = math.mul(rotation.Value, new float3(0f, 1f, 0f));

                translation.Value += direction * Time.DeltaTime * playerData.movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                quaternion zRot = quaternion.RotateZ( playerData.angularSpeed* Mathf.Deg2Rad * Time.DeltaTime);
                rotation.Value = math.mul(rotation.Value, zRot);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                quaternion zRot = quaternion.RotateZ(-playerData.angularSpeed * Mathf.Deg2Rad * Time.DeltaTime);
                rotation.Value = math.mul(rotation.Value, zRot);
            }
        });
    }
}
