using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlugEVMe.Exceptions;
using PlugEVMe.Models;
using PlugEVMe.Services;

using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using PlugEVMe.Generators.MapTools;

namespace PlugEVMe.ViewModels
{
    public class DetailViewModel : BaseViewModel<PlugEVMeEntry>
    {
        PlugEVMeEntry _entry;

        public PlugEVMeEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }
        
        public DetailViewModel(INavService navService, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
        }

        public override async Task Init(PlugEVMeEntry logEntry)
        {
            AnalyticsService.TrackEvent("Entry Detail Page", new Dictionary<string, string>
            {
                { "Title", logEntry.Title }
            });

            Entry = logEntry;
        }
    }
}