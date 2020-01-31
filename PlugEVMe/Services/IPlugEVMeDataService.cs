using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlugEVMe.Models;

namespace PlugEVMe.Services
{
    public interface IPlugEVMeDataService
    {
		Task<IList<PlugEVMeEntry>> GetEntriesAsync();
		Task<PlugEVMeEntry> GetEntryAsync(string id);
		Task<PlugEVMeEntry> AddEntryAsync(PlugEVMeEntry entry);
		Task<PlugEVMeEntry> UpdateEntryAsync(PlugEVMeEntry entry);
		Task RemoveEntryAsync(PlugEVMeEntry entry);
	}
}
