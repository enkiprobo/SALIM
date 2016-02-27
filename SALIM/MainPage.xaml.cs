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
using Windows.UI.ViewManagement;

using Windows.UI.Notifications;
using NotificationsExtensions.Toasts; // NotificationsExtensions.Win10


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SALIM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            HalamanYangAktif.Navigate(typeof(CountDownUI));
        }

        private void JudulAplikasi_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HalamanYangAktif.Navigate(typeof(HalamanUtama));
        }

        private void TombolRiwayat_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HalamanYangAktif.Navigate(typeof(HalamanRiwayat));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuDiKiri.IsPaneOpen = !MenuDiKiri.IsPaneOpen;
        }

        // method-method yang dibuat sesuai kebutuhan
        /*private void setAppWindowSize()
        {
            var halamanSaatIni = ApplicationView.GetForCurrentView();
            if (halamanSaatIni.IsFullScreenMode) halamanSaatIni.ExitFullScreenMode();
            // defining the default and minimal size of the app window
            halamanSaatIni.SetPreferredMinSize(new Size(500, 500));
            ApplicationView.PreferredLaunchViewSize = new Size(500, 700);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }*/
    }
}