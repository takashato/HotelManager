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
using System.Windows.Shapes;

using System.Data;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for ThuePhong.xaml
    /// </summary>
    public partial class ThuePhong : Window
    {
        //List<KhachHang> khachHangs;
        public ThuePhong()
        {
            InitializeComponent();
            //khachHangs = new List<KhachHang>(3);
            InitTable();
        }

        private void InitTable()
        {
            for(int i = 0;i<3;i++)
            {
                KhachHang khachHang = new KhachHang();
                khachHang.tenKH = "khach hang " + i;
                khachHang.loaiKH = "a";
                khachHang.CMND = "00";
                khachHang.DiaChi = "Tran Duy Hung";
                lstKhachHang.Items.Add(khachHang);
            }
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = this as Window;
            if (window != null)
            {
                window.DragMove();
            }
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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


    }

    public class KhachHang
    {
        public string tenKH { get; set; }
        public string loaiKH { get; set; }
        public string CMND { get; set; }
        public string DiaChi { get; set; }
    }
}
