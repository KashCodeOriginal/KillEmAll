using ECS.Guns.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Authoring
{
    public class BulletSpawnPointTagMono : MonoBehaviour
    {
        private class BulletSpawnPointTagBaker : Baker<BulletSpawnPointTagMono>
        {
            public override void Bake(BulletSpawnPointTagMono authoring)
            {
                AddComponent<BulletSpawnPointTag>();
            }
        }
    }
}