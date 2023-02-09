using UnityEngine;
using System.Collections.Generic;

namespace Other.Data.Static
{
    [CreateAssetMenu(fileName = "GunsData", menuName = "Data/GunsData", order = 0)]
    public class GunsData : ScriptableObject
    {
        [field: SerializeField] public List<GunStaticData> Guns { get; private set; }
    }
}