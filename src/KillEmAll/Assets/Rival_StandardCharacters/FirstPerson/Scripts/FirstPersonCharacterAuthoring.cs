using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Authoring;
using UnityEngine;
using Rival;
using Unity.Physics;
using System.Collections.Generic;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
[RequireComponent(typeof(PhysicsShapeAuthoring))]
public class FirstPersonCharacterAuthoring : MonoBehaviour
{
    public GameObject ViewEntity;
    public AuthoringKinematicCharacterProperties CharacterProperties = AuthoringKinematicCharacterProperties.GetDefault();
    public FirstPersonCharacterComponent Character = FirstPersonCharacterComponent.GetDefault();

    public class Baker : Baker<FirstPersonCharacterAuthoring>
    {
        public override void Bake(FirstPersonCharacterAuthoring authoring)
        {
            KinematicCharacterUtilities.BakeCharacter(this, authoring, authoring.CharacterProperties);

            authoring.Character.ViewEntity = GetEntity(authoring.ViewEntity);
        
            AddComponent(authoring.Character);
            AddComponent(new FirstPersonCharacterControl());
        }
    }
}