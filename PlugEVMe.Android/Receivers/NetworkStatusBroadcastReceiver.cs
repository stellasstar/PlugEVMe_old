using Android.App;
using Android.Content;
using System;

namespace PlugEVMe.Receivers
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted })]
    public class NetworkStatusBroadcastReceiver : BroadcastReceiver
    {
        public object _broadcastReceiver { get; private set; }

        public event EventHandler ConnectionStatusChanged;

        public event EventHandler NetworkStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, EventArgs.Empty);
        }
    }

}
