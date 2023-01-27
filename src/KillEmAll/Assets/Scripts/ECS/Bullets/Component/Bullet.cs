using Unity.Entities;

namespace ECS.Bullets.Component
{
    public struct Bullet : IComponentData
    {
        public float Damage;
        public float LifeTime;
        public float Speed;
    }
}