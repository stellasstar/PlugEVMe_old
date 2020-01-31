using Newtonsoft.Json;
using PlugEVMe.Models;
using PlugEVMe.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlugEVMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        private bool _IsOn;
        public ICommand NavigateCommand { get; set; }

        MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
        
        public bool IsOn
        {
            get
            {
                return _IsOn;
            }
            set
            {
                _IsOn = value;
                string onText = "Plan Journey";
                string offText = "Sorry. Journey Planning doesn't work yet!";
                JourneyPlannerButton.Text = _IsOn ? onText : offText;
            }
        }
        
        void Looks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var look = (PlugEVMeEntry)e.Item;

            _vm.ViewCommand.Execute(look);

            // Clear selection
            looks.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize MainViewModel
            if (_vm != null)
            {
                await _vm.Init();
            }
        }

        protected void ShowEntriesButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Text = "Show was just clicked!";
            var looks = (ObservableCollection<PlugEVMeEntry>)this.looks.ItemsSource;
            var clone = JsonConvert.DeserializeObject<ObservableCollection<PlugEVMeEntry>>(JsonConvert.SerializeObject(looks));
            _vm.PinsCommand.Execute(clone);

        }
        
        protected void JourneyPlannerButton_Click(object sender, EventArgs e)
        {
            IsOn = !IsOn;
        }
    }
}