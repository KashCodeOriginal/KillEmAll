using System;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Data.Component
{
    [Serializable]
    public class GunViewConfigData : IComponentData
    {
        public string GunName;
        public Sprite GunSprite;
        
        public GameObject BulletPrefab;
        public GameObject GunPrefab;
    }
}