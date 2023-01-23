using Unity.Entities;

namespace ECS.Random
{
    public struct Random : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }
}