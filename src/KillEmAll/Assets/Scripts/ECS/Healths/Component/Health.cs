using Unity.Entities;

namespace ECS.Healths.Component
{
    public struct Health : IComponentData
    {
        public float Value;
    }
}