using Akavache;
using Newtonsoft.Json;
using PlugEVMe.Exceptions;
using PlugEVMe.Generators;
using PlugEVMe.Models;
using PlugEVMe.Services;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace PlugEVMe.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IBlobCache _cache;

        ObservableCollection<PlugEVMeEntry> _logEntries = new ObservableCollection<PlugEVMeEntry>();
        PlugEVMeEntry _homeentry;

        public ObservableCollection<PlugEVMeEntry> LogEntries
        {
            get { return _logEntries; }
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public PlugEVMeEntry homeEntry
        {
            get { return _homeentry; }
            set
            {
                _homeentry = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadHomeEntry()
        {
            Position CrossGeolocator;
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            try
            {

                if (!Plugin.Geolocator.CrossGeolocator.IsSupported) { CrossGeolocator = new Position(); }
                if (!Plugin.Geolocator.CrossGeolocator.Current.IsGeolocationEnabled) { CrossGeolocator = new Position(); }
                if (!Plugin.Geolocator.CrossGeolocator.Current.IsGeolocationAvailable) { CrossGeolocator = new Position(); }

                // perhaps use a flag variable to enable/disable returning locations
                var locator = Plugin.Geolocator.CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                CrossGeolocator = await locator.GetPositionAsync(TimeSpan.FromSeconds(120));

                PlugEVMeEntry entry = new PlugEVMeEntry();

                entry.Latitude = CrossGeolocator.Latitude;
                entry.Longitude = CrossGeolocator.Longitude;
                entry.Title = "Me";
                entry.Date = DateTime.Now;
                entry.Id = "1";


                if (CrossGeolocator.Latitude != 0.0 && CrossGeolocator.Longitude != 0.0)
                {
                    Double lat = Convert.ToDouble(CrossGeolocator.Latitude);
                    Double lon = Convert.ToDouble(CrossGeolocator.Longitude);
                    var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);
                    var placemark = placemarks?.FirstOrDefault();
                    entry.Notes = placemark.ToString();
                    entry.Address = placemark;
                }
                homeEntry = entry;
            }

            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException is thrown ! " + e.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something is thrown ! " + e.ToString());
            }
            finally
            {
                CrossGeolocator = null;
                IsBusy = false;
            }

        }

        Command<ObservableCollection<PlugEVMeEntry>> _pinsCommand;
        public Command<ObservableCollection<PlugEVMeEntry>> PinsCommand
        {
            get
            {
                _pinsCommand = new Command<ObservableCollection<PlugEVMeEntry>>(async (entries) => await ExecutePinsCommand(entries));
                ((Command)_pinsCommand).CanExecute(true);
                return _pinsCommand;
            }
        }

        Command<PlugEVMeEntry> _viewCommand;
        public Command<PlugEVMeEntry> ViewCommand
        {
            get
            {
                return _viewCommand ?? (_viewCommand = new Command<PlugEVMeEntry>(async (entry) => await ExecuteViewCommand(entry)));
            }
        }
        
        Command _newCommand;
    
        public Command NewCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new Command(async () => await ExecuteNewCommand()));
            }
        }


        Command _refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));
            }
        }

        public MainViewModel(INavService navService,
            IBlobCache cache, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
            _cache = cache;
        }

        public override async Task Init()
        {
            await LoadHomeEntry();
            LoadEntries();
        }

        void LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = false;  
            LogEntries.Clear();

            try
            {
                ReadLibraries readLibraries = new ReadLibraries();
                List<string> libraries = readLibraries.libraries;

                // Load the current position homeentry and test values
                LogEntries.Insert(0, homeEntry);
                foreach (string library in libraries)
                {
                    PlugEVMeEntry _entry = new PlugEVMeEntry();

                    string[] libs = library.Split(',');

                    _entry.Longitude = Convert.ToDouble(libs[libs.Length - 1]);
                    _entry.Latitude = Convert.ToDouble(libs[libs.Length - 2]);
                    libs.Take(libs.Length - 1);
                    libs.Take(libs.Length - 2);

                    _entry.Title = libs[0];
                    _entry.Date = DateTime.Now;
                    _entry.Title = string.Join(" ", libs);
                    _entry.Notes = _entry.Latitude + " " +_entry.Longitude;
                    LogEntries.Add(_entry);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecutePinsCommand(ObservableCollection<PlugEVMeEntry> entries)
        {
            try
            {
                var clone = JsonConvert.DeserializeObject<ObservableCollection<PlugEVMeEntry>>(JsonConvert.SerializeObject(entries));
                await NavService.NavigateTo<PinItemsSourcePageViewModel, ObservableCollection<PlugEVMeEntry>>(clone);
            }
            catch (AppException e)
            {
                Debug.WriteLine("In catch block of Main method.");
                Debug.WriteLine("Caught: {0}", e.Message);
                if (e.InnerException != null)
                    Debug.WriteLine("Inner exception: {0}", e.InnerException);
            }
        }


        async Task ExecuteViewCommand(PlugEVMeEntry entry)
        {
            await NavService.NavigateTo<DetailViewModel, PlugEVMeEntry>(entry);
        }

        async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo<NewEntryViewModel>();
        }
    }
}
