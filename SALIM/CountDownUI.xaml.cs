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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SALIM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class CountDownUI : Page
    {

        DispatcherTimer countdown = new DispatcherTimer();
        TimeSpan waktuNya = new TimeSpan(0, 0, 5);
        public CountDownUI()
        {
            this.InitializeComponent();

            CountdownNya.Text = waktuNya.ToString(@"mm\:ss");
            countdown.Tick += Countdown_Tick;
            countdown.Interval = TimeSpan.FromSeconds(1);
            countdown.Start();
        }

        private void Countdown_Tick(object sender, object e)
        {
                waktuNya = waktuNya.Subtract(TimeSpan.FromSeconds(1));
                CountdownNya.Text = waktuNya.ToString(@"mm\:ss");
            if (waktuNya.CompareTo(new TimeSpan(0, 0, 0)) == 0) countdown.Stop();  
        }
    }
}
