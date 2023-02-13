using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Movement.Component
{
    public readonly partial struct DirectMoveAspect : IAspect
    {
        public readonly Entity Self;
        
        public readonly TransformAspect TransformAspect;
        
        private readonly RefRO<Speed> _speed;
        private readonly RefRW<DirectMove> _directMove;

        public float3 Direction
        {
            get => _directMove.ValueRO.Direction;
            set => _directMove.ValueRW.Direction = value;
        }
        
        public float DistanceFromDirection
        {
            get => _directMove.ValueRO.DistanceFromTarget;
            set => _directMove.ValueRW.DistanceFromTarget = value;
        }
        
        public float SafetyDistanceFromDirection
        {
            get => _directMove.ValueRO.SafetyDistanceFromTarget;
            set => _directMove.ValueRW.SafetyDistanceFromTarget = value;
        }
        
        public bool IsTargetReached => DistanceFromDirection <= _directMove.ValueRO.SafetyDistanceFromTarget;
        
        public float Speed => _speed.ValueRO.MoveSpeed;
    }
}