using ECS.Enemy.Spawn.Component;
using Other.Data;
using Other.Data.Static;
using Other.Services.ServiceLocator;
using Other.Services.StaticDataService;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Enemy.Spawn.System
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct EnemySpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }

        public void OnDestroy(ref SystemState state) { }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var enemySpawnAspect in SystemAPI.Query<EnemySpawnAspect>())
            {
                enemySpawnAspect.EnemySpawnTimer -= deltaTime;

                if (!enemySpawnAspect.TimeToSpawnEnemy)
                {
                    return;
                }

                var gunData = AllServices.Container.Single<IStaticDataService>().GetGunData(GunID.Ak47);
                
                Debug.Log(gunData.GunConfigData.Damage);

                var enemyEntity = ecb.Instantiate(enemySpawnAspect.EnemyPrefab);
            
                ecb.SetComponent(enemyEntity, new LocalTransform()
                {
                    Position = enemySpawnAspect.GetRandomPosition(),
                    Scale = 1f
                });

                enemySpawnAspect.EnemySpawnTimer = enemySpawnAspect.EnemySpawnRate;
            }
        }
    }
}