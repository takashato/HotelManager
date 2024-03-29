﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using MaterialDesignThemes.Wpf;
using HotelManager.gui.dialog;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl CurrentActivatedView { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CurrentActivatedView = _RoomListUC;
        }

        /// <summary>
        /// Force MainWindow update its contents
        /// </summary>
        private void Update()
        {
            if (App.Instance._Session == null) return; // Do not update when is not logged in!!!

            // Update Views
            _RoomListUC.LoadFromDB();

            // Update Staff infos
            txtStaff.Text = App.Instance._Session.CurrentStaff.Fullname;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Update();
            Console.WriteLine("MainWindow loaded.");
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int listItemSelectedIndex = LbxMenu.SelectedIndex;

            transitioningContentSlide.OnApplyTemplate();
            GrdCursor.Margin = new Thickness(10, (10 + (86 * listItemSelectedIndex)), 0, 0);

            UserControl ToDisplay = null;

            switch (listItemSelectedIndex)
            {
                case 0:
                    ToDisplay = _RoomListUC;
                    break;
                case 1:
                    if(App.Instance._Session.CurrentStaff.Level.ToString().Equals("Receptionist"))
                        MessageBox.Show("Bạn không có quyền truy cập!",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    else
                        ToDisplay = _RegulationUC;
                    break;
                case 2:
                    ToDisplay = _ReportUC;
                    break;
                case 3:
                    if (App.Instance._Session.CurrentStaff.Level.ToString().Equals("Administrator"))
                        ToDisplay = _AccountUC;
                    else
                        MessageBox.Show("Bạn không có quyền truy cập!",
                                        "Warning",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    break;
                case 4:
                    ToDisplay = _AboutUC;
                    break;
            }

            if (ToDisplay != null && ToDisplay != CurrentActivatedView)
            {
                CurrentActivatedView.Visibility = Visibility.Collapsed; // Hide current view
                CurrentActivatedView = ToDisplay;
                CurrentActivatedView.Visibility = Visibility.Visible;
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            (new ChangeAccountWindow()).ShowDialog();
        }

        public void ShowViewLoading()
        {
            viewDlgHost.ShowDialog(new LoadingDialog());
        }

        public void ShowMessage(string message)
        {
            viewDlgHost.ShowDialog(new MessageDialog(message));
        }

        public async void CloseViewDialog()
        {
            await Task.Delay(200);
            viewDlgHost.IsOpen = false;
        }
    }
}
