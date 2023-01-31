using ECS.Enemy.Spawn.Component;
using ECS.Environment.Component;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = ECS.Environment.Component.Random;

namespace ECS.Enemy.Spawn.Authoring
{
    public class EnemySpawnMono : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float3 _fieldSize;

        private uint _randomSeed;

        private void Awake()
        {
            _randomSeed = (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue);
        }

        private class EnemySpawnBaker : Baker<EnemySpawnMono>
        {
            public override void Bake(EnemySpawnMono authoring)
            {
                AddComponent(new EnemySpawn()
                {
                    EnemyPrefab = GetEntity(authoring._enemyPrefab),
                    SpawnRate = authoring._spawnRate,
                    FieldSize = authoring._fieldSize
                });
                
                AddComponent(new Random()
                {
                    Value = Unity.Mathematics.Random.CreateFromIndex(authoring._randomSeed)
                });

                AddComponent(new Timer());
            }
        }
    }
}