﻿using ECS.Environment.Component;
using ECS.Guns.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Authoring
{
    public class GunMono : MonoBehaviour
    {
        [SerializeField] private int _ammo;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _fireRate;
        
        
        private class GunBaker : Baker<GunMono>
        {
            public override void Bake(GunMono authoring)
            {
                AddComponent(new Gun()
                {
                    Ammo = authoring._ammo,
                    MaxAmmo = authoring._maxAmmo,
                    ReloadTime = authoring._reloadTime,
                    FireRate = authoring._fireRate
                });
                
                AddComponent<Timer>();
            }
        }
    }
}