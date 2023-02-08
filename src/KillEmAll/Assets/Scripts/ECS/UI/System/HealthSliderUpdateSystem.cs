using ECS.Healths.Component;
using ECS.UI.Component;
using Unity.Entities;

namespace ECS.UI.System
{
    public partial struct HealthSliderUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (healthAspect, healthBarUIData) in SystemAPI.Query<HealthAspect, HealthBarUIData>())
            {
                healthBarUIData.HealthBar.fillAmount = healthAspect.Health / healthAspect.MaxHealth;
            }
        }
    }
}