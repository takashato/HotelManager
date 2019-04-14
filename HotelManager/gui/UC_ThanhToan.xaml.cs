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
using System.Collections.ObjectModel;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for UC_ThanhToan.xaml
    /// </summary>
    
    public partial class UC_ThanhToan : UserControl
    {
        ObservableCollection<Phong> phongs = new ObservableCollection<Phong>();
        public UC_ThanhToan()
        {
            InitializeComponent();
            TablePhong.ItemsSource = phongs;
        }

        public UC_ThanhToan(List<string> LstPhong)
        {
            for (int i = 0; i < LstPhong.Count; i++)
            {
                Phong phong = new Phong();
                phong.SoPhong = LstPhong[i];
                phong.SoNgayThue = 0;
                phong.DonGia = 0;
                phong.ThanhTien = 0;
                phongs.Add(phong);
            }

            InitializeComponent();
            TablePhong.ItemsSource = phongs;
        }

        public class Phong
        {
            public string SoPhong { get; set; }
            public int SoNgayThue { get; set; }
            public long DonGia { get; set; }
            public long ThanhTien { get; set; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ChonPhongThanhToan chonPhong = new ChonPhongThanhToan(phongs);
            chonPhong.Show();
        }
    }
}
