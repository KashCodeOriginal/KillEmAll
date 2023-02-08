using Unity.Entities;
using UnityEngine;

namespace ECS.Healths.Authoring
{
    public class HealthMono : MonoBehaviour
    {
        [SerializeField] private float _value;
        [SerializeField] private float _maxValue;

        private class HealthBaker : Baker<HealthMono>
        {
            public override void Bake(HealthMono authoring)
            {
                AddComponent(new Component.Health()
                {
                    Value = authoring._value,
                    MaxValue = authoring._maxValue
                });
            }
        }
    }
}