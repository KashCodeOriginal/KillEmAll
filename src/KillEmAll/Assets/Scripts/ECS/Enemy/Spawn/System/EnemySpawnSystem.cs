using ECS.Enemy.Spawn.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Enemy.Spawn.System
{
    [BurstCompile]
    public partial struct EnemySpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            new EnemySpawnJob(deltaTime, ecb.AsParallelWriter()).ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct EnemySpawnJob : IJobEntity
    {
        private readonly float _deltaTime;
        private EntityCommandBuffer.ParallelWriter _ecb;

        public EnemySpawnJob(float deltaTime, EntityCommandBuffer.ParallelWriter ecb) : this()
        {
            _deltaTime = deltaTime;
            _ecb = ecb;
        }

        [BurstCompile]
        private void Execute(EnemySpawnAspect enemySpawnAspect, [EntityIndexInQuery] int entityKey)
        {
            enemySpawnAspect.EnemySpawnTimer -= _deltaTime;

            if (!enemySpawnAspect.TimeToSpawnEnemy)
            {
                return;
            }

            var enemyEntity = _ecb.Instantiate(entityKey, enemySpawnAspect.EnemyPrefab);
            
            _ecb.SetComponent(entityKey, enemyEntity, new LocalTransform()
            {
                Position = enemySpawnAspect.GetRandomPosition(),
                Scale = 1f
            });

            enemySpawnAspect.EnemySpawnTimer = enemySpawnAspect.EnemySpawnRate;
        }
    }
}