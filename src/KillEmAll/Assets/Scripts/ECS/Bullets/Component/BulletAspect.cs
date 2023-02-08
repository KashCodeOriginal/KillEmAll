using ECS.Environment.Component;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Bullets.Component
{
    public readonly partial struct BulletAspect : IAspect
    {
        public readonly Entity Self;
        
        public readonly TransformAspect TransformAspect;
        
        private readonly RefRO<Bullet> _bullet;
        private readonly RefRW<Timer> _timer;
        
        public float LifeTime => _bullet.ValueRO.LifeTime;
        public float Speed => _bullet.ValueRO.Speed;
        
        public float Timer
        {
            get => _timer.ValueRO.Value;
            set => _timer.ValueRW.Value = value;
        }
    }
}