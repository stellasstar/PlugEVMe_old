using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlugEVMe.Services
{
    public interface IHandsetDialogService
    {
        Task<bool> ShowAlert(string message, string title, string okButton, Action callback);
        Task<bool> ShowAlertConfirm(string message, string title, string confirmButton, string cancelButton, Action<bool> callback);
    }
}
