using UnityEngine;
using Unity.Entities;
using ECS.Movement.Component;

namespace ECS.Movement.Authoring
{
    public class DirectMoveMono : MonoBehaviour
    {
        [SerializeField] private float _safetyDistanceFromTarget;
        
        private class DirectMoveBaker : Baker<DirectMoveMono>
        {
            public override void Bake(DirectMoveMono authoring)
            {
                AddComponent(new DirectMove()
                {
                    SafetyDistanceFromTarget = authoring._safetyDistanceFromTarget
                });
            }
        }
    }
}