using ECS.UI.Component;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace ECS.UI.Authoring
{
    public class HealthBarMono : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private class HealthBarBaker : Baker<HealthBarMono>
        {
            public override void Bake(HealthBarMono authoring)
            {
                var healthBarUIData = new HealthBarUIData()
                {
                    HealthBar = authoring._healthBar
                };
                
                AddComponentObject(healthBarUIData);
            }
        }
    }
}