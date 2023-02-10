using UnityEngine;
using ECS.Guns.Data.Component;

namespace Other.Data.Static
{
    [CreateAssetMenu(fileName = "GunStaticData", menuName = "Data/GunStaticData", order = 0)]
    public class GunStaticData : ScriptableObject
    { 
         [field:SerializeField] public GunStatsConfigData GunStatsConfigData { get; private set; }
         [field:SerializeField] public GunViewConfigData GunViewConfigData { get; private set; }
    }
}