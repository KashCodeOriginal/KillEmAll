using ECS.Enemy.Refs.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Enemy.Refs.Authoring
{
    public class EnemyChildRefsMono : MonoBehaviour
    {
        [SerializeField] private GameObject _gun;
        
        private class EnemyChildRefsBaker : Baker<EnemyChildRefsMono>
        {
            public override void Bake(EnemyChildRefsMono authoring)
            {
                AddComponent(new EnemyChildRefs()
                {
                    Gun = GetEntity(authoring._gun)
                });
            }
        }
    }
}