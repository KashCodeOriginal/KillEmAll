using Unity.Entities;

namespace ECS.Raycast.Component
{
    public struct RayCast : IComponentData
    {
        public float Length;
        public uint CollidesWith;
        public uint BelongsTo;
        public int GroupIndex;
    }
}