using ECS.Environment.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Bullets.Authoring
{
    public class BulletMono : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        

        private class BulletBaker : Baker<BulletMono>
        {
            public override void Bake(BulletMono authoring)
            {
                AddComponent(new Component.Bullet()
                {
                    Damage = authoring._damage,
                    LifeTime = authoring._lifeTime,
                    Speed = authoring._speed
                });
                
                AddComponent<Timer>();
            }
        }
    }
}