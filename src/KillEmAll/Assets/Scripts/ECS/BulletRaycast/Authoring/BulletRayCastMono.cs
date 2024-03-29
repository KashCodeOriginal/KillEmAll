﻿using ECS.BulletRaycast.Component;
using Unity.Entities;
using Unity.Physics.Authoring;
using UnityEngine;

namespace ECS.BulletRaycast.Authoring
{
    public class RayCastMono : MonoBehaviour
    {
        [SerializeField] private int _length;
        [SerializeField] private PhysicsCategoryTags  _collidesWith;
        [SerializeField] private PhysicsCategoryTags  _belongsTo;
        [SerializeField] private int _groupIndex;
        
        private class RayCastBaker : Baker<RayCastMono>
        {
            public override void Bake(RayCastMono authoring)
            {
                AddComponent(new RayCast()
                {
                    Length = authoring._length,
                    CollidesWith = authoring._collidesWith.Value,
                    BelongsTo = authoring._belongsTo.Value,
                    GroupIndex = authoring._groupIndex
                });
            }
        }
    }
}