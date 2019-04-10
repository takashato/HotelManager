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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            InitTable();
        }
        private void InitTable()
        {
            for (int i = 0; i < 3; i++)
            {
                KhachHang khachHang = new KhachHang();
                khachHang.tenKH = "khach hang " + i;
                khachHang.loaiKH = "a";
                khachHang.CMND = "00";
                khachHang.DiaChi = "Tran Duy Hung";
                lstKhachHang.Items.Add(khachHang);
            }
        }
    }
}
