using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Movement.Component
{
    public struct DirectMove : IComponentData
    {
        public float3 Direction;
        
        public float DistanceFromTarget;
        
        public float SafetyDistanceFromTarget;
    }
}