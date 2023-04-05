using System.Collections.Generic;
using System.Linq;
using Other.Data;
using Other.Data.Static;
using UnityEngine;

namespace Other.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string GUNS_STATIC_DATA_PATH = "Static/Guns";

        private Dictionary<GunID, GunStaticData> _guns;

        public void LoadStaticData()
        {
            _guns = Resources
                .LoadAll<GunStaticData>(GUNS_STATIC_DATA_PATH)
                .ToDictionary(x => x.GunConfigData.GunID, x => x);
        }

        public GunStaticData GetGunData(GunID gunID)
        {
            return _guns.TryGetValue(gunID, out GunStaticData staticData) ? staticData : null;
        }
    }
}