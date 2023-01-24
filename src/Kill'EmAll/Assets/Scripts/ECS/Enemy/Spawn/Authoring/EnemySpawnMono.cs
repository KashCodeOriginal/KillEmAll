using System;
using ECS.Enemy.Spawn.Component;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Enemy.Spawn.Authoring
{
    public class EnemySpawnMono : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float3 _fieldSize;

        [SerializeField] private uint _randomSeed;

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
                
                AddComponent(new Random.Component.Random()
                {
                    Value = Unity.Mathematics.Random.CreateFromIndex(authoring._randomSeed)
                });

                AddComponent(new EnemySpawnTimer());
            }
        }
    }
}