using ECS.Enemy.Shoot.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Enemy.Shoot.Authoring
{
    public class EnemyShootMono : MonoBehaviour
    {
        private class EnemyShootBaker : Baker<EnemyShootMono>
        {
            public override void Bake(EnemyShootMono authoring)
            {
                AddComponent<EnemyShoot>();
            }
        }
    }
}