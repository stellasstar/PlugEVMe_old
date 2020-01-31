using PlugEVMe.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PlugEVMe.Services
{
	public interface INavService
	{
		event PropertyChangedEventHandler CanGoBackChanged;

		bool CanGoBack { get; }

		Task GoBack();
		Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;
		Task RemoveLastView();
		Task ClearBackStack();
		Task NavigateToUri(Uri uri);
        Task NavigateTo<T>();
    }
}
