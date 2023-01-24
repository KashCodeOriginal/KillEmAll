using Unity.Entities;

namespace ECS.Random.Component
{
    public struct Random : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }
}