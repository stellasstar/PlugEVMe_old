using Ninject.Modules;
using PlugEVMe.Droid.Services;
using PlugEVMe.Services;

namespace PlugEVMe.Droid.Modules
{
    public class PlugEVMePlatformModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ILocationService>().To<LocationService>().InSingletonScope();
		}
	}
}