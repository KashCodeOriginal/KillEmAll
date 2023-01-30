using Unity.Entities;

namespace ECS.Damage.Component
{
    public struct MakeDamage : IComponentData
    {
        public Entity Self;
        
        public Entity Target;
        public float Value;
    }
}