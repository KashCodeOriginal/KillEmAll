using Unity.Entities;

namespace ECS.Healths.Component
{
    public readonly partial struct HealthAspect : IAspect
    {
        public readonly Entity Self;
        
        private readonly RefRW<Health> _health;

        public float Health
        {
            get => _health.ValueRW.Value;
            set => _health.ValueRW.Value = value;
        }
    }
}