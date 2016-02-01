using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using Windows.UI.Notifications;
using NotificationsExtensions.Toasts; // NotificationsExtensions.Win10
using Microsoft.QueryStringDotNET; // QueryString.NET

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SALIM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HalamanUtama : Page
    {
        public HalamanUtama()
        {
            this.InitializeComponent();
        }

        private void KalenderPerbulan_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var selectedDate = sender.SelectedDates.Select(p => p.Date.Day.ToString() + "/" + p.Date.Month.ToString() + "/" + p.Date.Year.ToString()).ToArray();
            var dateSelect = string.Join(",", selectedDate);
            HariIni.Text = dateSelect;
        }
    }
}
