using Unity.Entities;

namespace ECS.Environment.Component
{
    public struct Random : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }
}