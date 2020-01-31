using Ninject;
using Ninject.Modules;
using PlugEVMe.Modules;
using PlugEVMe.ViewModels;
using PlugEVMe.Views;
using Xamarin.Forms;

namespace PlugEVMe
{
    public partial class App : Application
    {
        [Inject]
        public IKernel Kernel { get; protected set; }

        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            ThemeManager.LoadTheme();
            var mainPage = new NavigationPage(new MainPage());
            var modules = new INinjectModule[]
                                           {
                                               new PlugEVMeCoreModule(),
                                               new PlugEVMeNavModule(mainPage.Navigation),
                                           };
            var settings = new NinjectSettings();
            settings.LoadExtensions = false;

            // Register core services
            Kernel = new StandardKernel(settings, modules);

            // Register platform specific services
            Kernel.Load(platformModules);

            // Get the MainViewModel from the IoC
            mainPage.BindingContext = Kernel.Get<MainViewModel>();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
