using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Enemy.Spawn.Component
{
    public struct EnemySpawn : IComponentData
    {
        public Entity EnemyPrefab;
        public float SpawnRate;
        public float3 FieldSize;
    }
}