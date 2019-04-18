using HotelManager.data;
using HotelManager.db;
using HotelManager.gui;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Return Application Instance itself
        /// </summary>
        public static App Instance
        {
            get
            {
                return Application.Current as App;
            }
        }

        public MainWindow _MainWindow { get; set; }
        public LoginWindow _LoginWindow { get; set; }

        public Session _Session { get; set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            DatabaseManager.Instance.Initialize();

            _MainWindow = new MainWindow();
            _LoginWindow = new LoginWindow();

            _LoginWindow.Show();
        }

        private void OnLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Console.WriteLine("Load completed!");
        }
    }
}
