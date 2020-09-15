
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using Unity.Mathematics;
using System.Runtime.InteropServices;
using Unity.Physics;

public class GameController : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private UnityEngine.Material player_material;
    [SerializeField] private UnityEngine.Material asteroid_material;

    [SerializeField] private GameObject AsteroidPrefab;

    private EntityManager entityManager;
    private World world;

    private void Awake()
    {
        world = World.DefaultGameObjectInjectionWorld;
        entityManager = world.EntityManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        BlobAssetStore blob = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, blob);
        var AsteroidPrefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(AsteroidPrefab, settings);

        InstantiateGrid(AsteroidPrefabEntity, 160, 160, 3.0f);
    }

    private void InstantiateEntity(Entity prefab, float3 pos)
    {
        Entity Entity = entityManager.Instantiate(prefab);
        entityManager.SetComponentData(Entity, new Translation
        {
            Value = pos
        });
        entityManager.SetComponentData(Entity, new PhysicsVelocity
        {
            Linear = new float3(UnityEngine.Random.Range(-1.0f,1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0)
           
        });

    }
    private void InstantiateGrid(Entity prefab, int GridX, int GridY, float Spacing)
    {
        for(int i=0; i<GridX; i++)
        {
            for (int j = 0; j < GridY; j++)
            {
                InstantiateEntity(prefab, new float3(Spacing * (i-(GridX/2.0f)), Spacing * (j - (GridY / 2.0f)), 0));
            }
        }
    }



}

