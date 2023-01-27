using Unity.Entities;

namespace ECS.BulletRaycast.Component
{
    public struct RayCast : IComponentData
    {
        public int Length;
        public uint CollidesWith;
        public uint BelongsTo;
        public int GroupIndex;
    }
}