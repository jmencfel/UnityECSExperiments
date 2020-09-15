using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct PlayerDataComponent : IComponentData
{
    public float movementSpeed;
    public float angularSpeed;
}
