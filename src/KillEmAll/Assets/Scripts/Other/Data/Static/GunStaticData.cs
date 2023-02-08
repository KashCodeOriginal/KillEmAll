using UnityEngine;

namespace Other.Data.Static
{
    [CreateAssetMenu(fileName = "GunStaticData", menuName = "Data/GunStaticData", order = 0)]
    public class GunStaticData : ScriptableObject
    {
        [field: SerializeField] public GunType GunType { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public string GunName { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    }
}