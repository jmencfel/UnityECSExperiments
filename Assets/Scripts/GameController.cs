
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


    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject PlayerPrefab;

    BlobAssetStore blob;

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
        blob = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, blob);
        Entity AsteroidPrefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(AsteroidPrefab, settings);
        Entity PlayerPrefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(PlayerPrefab, settings);
        Entity BulletPrefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, settings);

        
        InstantiateGrid(AsteroidPrefabEntity, 160, 160, 3.0f);
        Entity player = InstantiateEntity(PlayerPrefabEntity, new float3(0, 0, 0));

        entityManager.SetComponentData(player, new PlayerDataComponent
        {
            angularSpeed = 180,
            movementSpeed = 5,
            entityManager = entityManager,
            BulletPrefab = BulletPrefabEntity
        });



    }

    private Entity InstantiateEntity(Entity prefab, float3 pos)
    {
        Entity Entity = entityManager.Instantiate(prefab);
        entityManager.SetComponentData(Entity, new Translation
        {
            Value = pos
        });
       
        return Entity;
    }
    private void InstantiateGrid(Entity prefab, int GridX, int GridY, float Spacing)
    {
        for(int i=0; i<GridX; i++)
        {
            for (int j = 0; j < GridY; j++)
            {
               var Entity = InstantiateEntity(prefab, new float3(Spacing * (i-(GridX/2.0f)) -Spacing/2.0f, Spacing * (j - (GridY / 2.0f)) - Spacing / 2.0f, 0));
                float x = UnityEngine.Random.Range(-1.0f, 1.0f);
                float y = UnityEngine.Random.Range(-1.0f, 1.0f);
                entityManager.SetComponentData(Entity, new PhysicsVelocity
                {
                    Linear = new float3(x, y, 0)

                });
                entityManager.SetComponentData(Entity, new AsteroidComponent
                {
                    startVelocity = entityManager.GetComponentData<PhysicsVelocity>(Entity).Linear

                });
            }
        }
    }

    private void OnDestroy()
    {
        blob.Dispose();
    }

}

