using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PlugEVMe.Models;
using PlugEVMe.Services;

namespace PlugEVMe.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected INavService NavService { get; private set; }

        protected IAnalyticsService AnalyticsService { get; private set; }

        bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                OnIsBusyChanged();
            }
        }

        public abstract Task Init();

        public BaseViewModel(INavService navService, IAnalyticsService analyticsService)
        {
            NavService = navService;
            AnalyticsService = analyticsService;
        }
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;

            Debug.WriteLine("NotifyPropertyChanged: " + propertyName);

            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnIsBusyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INavService navService, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
        }

        public override async Task Init()
        {
            await Init(default(TParameter));
        }

        public abstract Task Init(TParameter parameter);
    }
}