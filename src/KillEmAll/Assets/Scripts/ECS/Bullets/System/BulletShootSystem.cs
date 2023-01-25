using ECS.Bullets.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Bullets.System
{
    [BurstCompile]
    public partial struct BulletShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            new BulletShotJob(deltaTime, ecb).ScheduleParallel();
        }
    }
    
    public partial struct BulletShotJob : IJobEntity
    {
        private readonly float _deltaTime;
        private EntityCommandBuffer.ParallelWriter _ecb;

        public BulletShotJob(float deltaTime, EntityCommandBuffer.ParallelWriter ecb) : this()
        {
            _deltaTime = deltaTime;
            _ecb = ecb;
        }

        private void Execute(BulletAspect bulletAspect, [EntityIndexInQuery] int entityIndex)
        {
            bulletAspect.Timer += _deltaTime;

            if (bulletAspect.Timer >= bulletAspect.LifeTime)
            {
                _ecb.DestroyEntity(entityIndex, bulletAspect.Self);
            }

            bulletAspect.TransformAspect.LocalPosition +=  math.forward(bulletAspect.TransformAspect.LocalRotation) * bulletAspect.Speed * _deltaTime;
        }
    }
}