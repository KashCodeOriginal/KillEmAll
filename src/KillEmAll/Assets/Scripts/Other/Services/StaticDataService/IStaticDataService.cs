using Other.Data;
using Other.Data.Static;

namespace Other.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        public void LoadStaticData();
        public GunStaticData GetGunData(GunID gunID);
    }
}