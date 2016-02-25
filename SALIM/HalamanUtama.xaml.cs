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

using SQLite.Net;
using System.Globalization;
using Windows.UI.Notifications;
using NotificationsExtensions.Toasts; // NotificationsExtensions.Win10
using Microsoft.QueryStringDotNET; // QueryString.NET
// use Install-Package SQLite.Net.Async-PCL
// use Install-Package SQLite.Net-PCL

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SALIM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HalamanUtama : Page
    {
        SQLiteConnection koneksiKeDatabaseJadwalSholatSQLite = App.konek;
        CultureInfo kulturIndonesia = new CultureInfo("id-ID");
        DateTime sekarang = DateTime.Now;
        jadwalSholat jadwalSholatHariIni;
        DispatcherTimer jamMenitDetik = new DispatcherTimer();
        String waktuSalatYangAkanDatang;
        Windows.Data.Xml.Dom.XmlDocument Notifikasinya;
        public HalamanUtama()
        {
            this.InitializeComponent();

            jamMenitDetik.Tick += JamMenitDetik_Tick;
            jamMenitDetik.Interval = TimeSpan.FromSeconds(1);
            jamMenitDetik.Start();

            var query = koneksiKeDatabaseJadwalSholatSQLite.Table<jadwalSholat>();
            jadwalSholatHariIni = (from jadwalSholat u in query
                                   where u.tanggal == sekarang.ToString("d/M/yyyy")
                                   select u).FirstOrDefault();

            HariIni.Text = sekarang.ToString("dd MMMM yyyy", kulturIndonesia);
            JamHariIni.Text = sekarang.ToString("HH:mm:ss");
            
            WaktuSubuh.Text = jadwalSholatHariIni.fajr;
            WaktuZuhur.Text = jadwalSholatHariIni.zuhr;
            WaktuAshar.Text = jadwalSholatHariIni.asr;
            WaktuMaghrib.Text = jadwalSholatHariIni.maghrib;
            WaktuIsya.Text = jadwalSholatHariIni.isha;

        }

        private void JamMenitDetik_Tick(object sender, object e)
        {
            sekarang = DateTime.Now;
            JamHariIni.Text = sekarang.ToString("HH:mm:ss");
            updateWaktuSholat();
        }

        private void KalenderPerbulan_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var dateSelect = sender.SelectedDates.Select(p => p.Date).FirstOrDefault();
            HariIni.Text = dateSelect.ToString("dd MMMM yyyy", kulturIndonesia);

            var query = koneksiKeDatabaseJadwalSholatSQLite.Table<jadwalSholat>();
            var ini = (from jadwalSholat u in query
                       where u.tanggal == dateSelect.ToString("d/M/yyyy")
                       select u).FirstOrDefault();
            if(ini != null)
            {
                WaktuSubuh.Text = ini.fajr;
                WaktuZuhur.Text = ini.zuhr;
                WaktuAshar.Text = ini.asr;
                WaktuMaghrib.Text = ini.maghrib;
                WaktuIsya.Text = ini.isha;
            }
        }
        void updateWaktuSholat()
        {
            var tempSubuh = Convert.ToDateTime(jadwalSholatHariIni.fajr);
            var tempZuhur = Convert.ToDateTime(jadwalSholatHariIni.zuhr);
            var tempAshar = Convert.ToDateTime(jadwalSholatHariIni.asr);
            var tempMaghrib = Convert.ToDateTime(jadwalSholatHariIni.maghrib);
            var tempIsha = Convert.ToDateTime(jadwalSholatHariIni.isha);

            int membandingkanWaktudenganSubuh = sekarang.CompareTo(tempSubuh);
            int membandingkanWaktudenganZuhur = sekarang.CompareTo(tempZuhur);
            int membandingkanWaktudenganAshar = sekarang.CompareTo(tempAshar);
            int membandingkanWaktudenganMaghrib = sekarang.CompareTo(tempMaghrib);
            int membandingkanWaktudenganIsha = sekarang.CompareTo(tempIsha);

            int membandingkanWaktudenganSubuhKurang1Detik = sekarang.CompareTo(tempSubuh.Subtract(TimeSpan.FromSeconds(1)));
            int membandingkanWaktudenganZuhurKurang1Detik = sekarang.CompareTo(tempZuhur.Subtract(TimeSpan.FromSeconds(1)));
            int membandingkanWaktudenganAsharKurang1Detik = sekarang.CompareTo(tempAshar.Subtract(TimeSpan.FromSeconds(1)));
            int membandingkanWaktudenganMaghribKurang1Detik = sekarang.CompareTo(tempMaghrib.Subtract(TimeSpan.FromSeconds(1)));
            int membandingkanWaktudenganIshaKurang1Detik = sekarang.CompareTo(tempIsha.Subtract(TimeSpan.FromSeconds(1)));

            if (membandingkanWaktudenganSubuh < 0 || membandingkanWaktudenganIsha > 0)
            {
                NamaSholat.Text = "Subuh";
                waktuSalatYangAkanDatang = jadwalSholatHariIni.fajr;
                if(membandingkanWaktudenganIsha < 0)
                    if (membandingkanWaktudenganSubuhKurang1Detik > 0) ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(buatPilihanKeinginanSholatPada(NamaSholat.Text)));
            }
            else if (membandingkanWaktudenganMaghrib > 0)
            {
                NamaSholat.Text = "Isha";
                waktuSalatYangAkanDatang = jadwalSholatHariIni.isha;
                if (membandingkanWaktudenganIshaKurang1Detik > 0) ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(buatPilihanKeinginanSholatPada(NamaSholat.Text)));
            }
            else if(membandingkanWaktudenganAshar > 0)
            {
                NamaSholat.Text = "Maghrib";
                waktuSalatYangAkanDatang = jadwalSholatHariIni.maghrib;
                if (membandingkanWaktudenganMaghribKurang1Detik > 0) ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(buatPilihanKeinginanSholatPada(NamaSholat.Text)));
            } else if(membandingkanWaktudenganZuhur > 0)
            {
                NamaSholat.Text = "Ashar";
                waktuSalatYangAkanDatang = jadwalSholatHariIni.asr;
                if (membandingkanWaktudenganAsharKurang1Detik > 0) ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(buatPilihanKeinginanSholatPada(NamaSholat.Text)));
            } else if(membandingkanWaktudenganSubuh > 0)
            {
                NamaSholat.Text = "Zuhur";
                waktuSalatYangAkanDatang = jadwalSholatHariIni.zuhr;
                if (membandingkanWaktudenganZuhurKurang1Detik > 0) ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(buatPilihanKeinginanSholatPada(NamaSholat.Text)));
            }
            
        }
        Windows.Data.Xml.Dom.XmlDocument buatPilihanKeinginanSholatPada(String waktuSalatnya)
        {
            var stringNya = $@"<toast duration=""long"" scenario=""reminder"">
                                  <visual>
                                    <binding template=""ToastGeneric"">
                                      <text>Salat In Time</text>
                                      <text>Saatnya salat {waktuSalatnya}, yuk salat dulu</text>
                                      <image placement=""AppLogoOverride"" src=""oneAlarm.png"" />
                                    </binding>
                                  </visual>
                                  <actions>
                                    <action content=""Lanjutkan"" arguments=""check"" imageUri=""check.png"" />
                                    <action content=""Gak Ah"" arguments=""cancel"" />
                                  </actions>
                                  <audio src=""ms-winsoundevent:Notification.Reminder""/>
                               </toast>";
            var ubahStringkeXML = new Windows.Data.Xml.Dom.XmlDocument();
            ubahStringkeXML.LoadXml(stringNya);
            return ubahStringkeXML;
        }
    }
}
