using Unity.Entities;
using UnityEngine;

namespace ECS.Health.Authoring
{
    public class HealthMono : MonoBehaviour
    {
        [SerializeField] private float _value;
        
        private class HealthBaker : Baker<HealthMono>
        {
            public override void Bake(HealthMono authoring)
            {
                AddComponent(new Component.Health()
                {
                    Value = authoring._value
                });
            }
        }
    }
}