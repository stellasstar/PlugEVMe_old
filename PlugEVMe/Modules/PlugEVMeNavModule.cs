using System;
using Ninject.Modules;
using PlugEVMe.Services;
using PlugEVMe.Views;
using Xamarin.Forms;
using PlugEVMe.ViewModels;

namespace PlugEVMe.Modules
{
    public class PlugEVMeNavModule : NinjectModule
    {
		private INavigation _xfNav;

		public PlugEVMeNavModule(INavigation xamarinFormsNavigation)
		{
			_xfNav = xamarinFormsNavigation;
		}

		public override void Load()
		{
			var navService = new XamarinFormsNavService();
			navService.XamarinFormsNav = _xfNav;

			// Register view mappings
			navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
			navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
			navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));
			navService.RegisterViewMapping(typeof(PinItemsSourcePageViewModel), typeof(PinItemsSourcePage));

			Bind<INavService>().ToMethod(x => navService).InSingletonScope();
		}
	}
}
