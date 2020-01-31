using Newtonsoft.Json;
using PlugEVMe.Models;
using PlugEVMe.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PlugEVMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinItemsSourcePage : ContentPage
    {
        public PinItemsSourcePageViewModel _vm
        {
            get { return BindingContext as PinItemsSourcePageViewModel; }
        }

        public PinItemsSourcePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
            {
                _vm.IsBusy = true;
                map = new Map();
                map.IsVisible = true;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(53.8283459, -1.5794797), Distance.FromMiles(5000)));
                //map.MapClicked += OnMapClicked;
                //_vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(PinItemsSourcePageViewModel.Locations))
            {

                
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_vm != null)
            {
                _vm.IsBusy = false;
                _vm.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        }
    }
}
