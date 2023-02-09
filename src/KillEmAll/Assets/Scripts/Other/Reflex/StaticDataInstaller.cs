using Other.Data.Static;
using Reflex;
using Reflex.Scripts;
using UnityEngine;

namespace Other.Reflex
{
    public class StaticDataInstaller : Installer
    {
        [SerializeField] private GunsData _gunsData;
        
        public override void InstallBindings(Container container)
        {
            container.BindInstance(_gunsData);
        }
    }
}
