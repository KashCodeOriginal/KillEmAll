using UnityEngine;
using Unity.Entities;

[DisallowMultipleComponent]
public class FirstPersonPlayerAuthoring : MonoBehaviour
{
    public GameObject ControlledCharacter;
    public float MouseSensitivity = 1f;

    public class Baker : Baker<FirstPersonPlayerAuthoring>
    {
        public override void Bake(FirstPersonPlayerAuthoring authoring)
        {
            AddComponent(new FirstPersonPlayer
            {
                ControlledCharacter = GetEntity(authoring.ControlledCharacter),
                MouseSensitivity = authoring.MouseSensitivity,
            });
            AddComponent(new FirstPersonPlayerInputs());
        }
    }
}