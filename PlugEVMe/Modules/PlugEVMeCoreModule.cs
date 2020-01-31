using System;
using Ninject.Modules;
using PlugEVMe.Services;
using PlugEVMe.ViewModels;

namespace PlugEVMe.Modules
{
	public class PlugEVMeCoreModule : NinjectModule
	{
		public override void Load()
		{
			// ViewModels
			Bind<MainViewModel>().ToSelf();
			Bind<DetailViewModel>().ToSelf();
			Bind<NewEntryViewModel>().ToSelf();
			Bind<PinItemsSourcePageViewModel>().ToSelf();

			Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);

            Bind<IAnalyticsService>().To<AppCenterAnalyticsService>().InSingletonScope();

        }
	}
}
