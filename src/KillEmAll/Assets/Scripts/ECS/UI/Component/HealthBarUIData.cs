using Unity.Entities;
using UnityEngine.UI;

namespace ECS.UI.Component
{
    public class HealthBarUIData : IComponentData
    {
        public Image HealthBar;
    }
}