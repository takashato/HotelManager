using System;
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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            GrdContent.Children.Add(new DanhMucPhong());
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = this as Window;
            if (window != null)
            {
                window.DragMove();
            }
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

        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            gui.Window1 window1 = new gui.Window1();
            window1.Show();
        }

        private void Btn_ThuePhong_Click(object sender, RoutedEventArgs e)
        {
            gui.ThuePhong thuePhong = new gui.ThuePhong();
            thuePhong.Show();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int listItemSelectedIndex = LbxMenu.SelectedIndex;
            switch (listItemSelectedIndex)
            {
                case 0:
                    GrdContent.Children.Clear();
                    GrdContent.Children.Add(new DanhMucPhong());
                    break;
                case 1:
                    GrdContent.Children.Clear();
                    break;
                case 2:
                    GrdContent.Children.Clear();
                    GrdContent.Children.Add(new DanhMucPhong());
                    break;
                case 3:
                    GrdContent.Children.Clear();
                    break;
                case 4:
                    GrdContent.Children.Clear();
                    GrdContent.Children.Add(new DanhMucPhong());
                    break;
            }
        }
    }
}
