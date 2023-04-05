using Unity.Entities;

namespace ECS.Enemy.Refs.Component
{
    public struct EnemyChildRefs : IComponentData
    {
        public Entity Gun;
    }
}