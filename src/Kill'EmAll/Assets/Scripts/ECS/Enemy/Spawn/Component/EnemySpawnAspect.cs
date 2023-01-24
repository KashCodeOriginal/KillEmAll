using ECS.Enemy.Spawn.Authoring;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Enemy.Spawn.Component
{
    public readonly partial struct EnemySpawnAspect : IAspect
    {
        private readonly TransformAspect _transformAspect;
        
        private readonly RefRO<EnemySpawn> _enemySpawn;
        private readonly RefRW<Random.Component.Random> _random;
        private readonly RefRW<EnemySpawnTimer> _enemySpawnTimer;

        public bool TimeToSpawnEnemy => EnemySpawnTimer <= 0f;
        
        public float EnemySpawnTimer
        {
            get => _enemySpawnTimer.ValueRO.Value;
            set => _enemySpawnTimer.ValueRW.Value = value;
        }

        public float EnemySpawnRate => _enemySpawn.ValueRO.SpawnRate;

        public Entity EnemyPrefab => _enemySpawn.ValueRO.EnemyPrefab;

        public float3 GetRandomPosition()
        {
            float3 randomPosition = _random.ValueRW.Value.NextFloat3(_minCorner, _maxCorner);
            randomPosition.y = _enemySpawn.ValueRO.FieldSize.y;
            
            return randomPosition;
        }

        private float3 _minCorner => _transformAspect.LocalPosition - _halfDimensions;

        private float3 _maxCorner => _transformAspect.LocalPosition + _halfDimensions;

        private float3 _halfDimensions => new float3()
        {
            x = _enemySpawn.ValueRO.FieldSize.x / 2,
            y = 0f,
            z = _enemySpawn.ValueRO.FieldSize.z / 2
        };
    }
}
