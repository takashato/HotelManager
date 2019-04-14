using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for DanhMucPhong.xaml
    /// </summary>
    public partial class DanhMucPhong : UserControl
    {

        public ObservableCollection<ThongTinPhong> DanhSachPhongTest { get; set; } = new ObservableCollection<ThongTinPhong>();

        public DanhMucPhong()
        {
            InitializeComponent();
            DataContext = this;
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "A101", LoaiPhong = "A-Normal", DonGia = 1000000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "B5.12", LoaiPhong = "B-VIP", DonGia = 2500000M, TinhTrang = ThongTinPhong.TinhTrangPhong.DaThue });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C33", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.DaThue });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
            DanhSachPhongTest.Add(new ThongTinPhong() { Phong = "C303", LoaiPhong = "C-Normal", DonGia = 700000M, TinhTrang = ThongTinPhong.TinhTrangPhong.ConTrong });
        }
        public class ThongTinPhong
        {
            public enum TinhTrangPhong { ConTrong, DaThue }

            private string phong;
            private string loaiPhong;
            private decimal donGia;
            private TinhTrangPhong tinhTrang;

            public string Phong
            {
                get { return phong; }
                set { phong = value; }
            }

            public string LoaiPhong
            {
                get { return loaiPhong; }
                set { loaiPhong = value; }
            }

            public decimal DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }

            public TinhTrangPhong TinhTrang
            {
                get { return tinhTrang; }
                set { tinhTrang = value; }
            }

            public string DonGiaStr => string.Format("{0:N0}", DonGia);

            public string TinhTrangStr
            {
                get
                {
                    if (tinhTrang == TinhTrangPhong.ConTrong)
                        return "Còn trống";
                    if (tinhTrang == TinhTrangPhong.DaThue)
                        return "Đã thuê";
                    return "Chưa set??";
                }
            }

        }

        private void ThuePhong_Click(object sender, RoutedEventArgs e)
        {
            //Truyền tham số tên phòng vào Form Thuê Phòng
            int index = listview_DMPhong.SelectedIndex;
            if (index < 0)
                return;
            string sophong = "Phòng " + DanhSachPhongTest[index].Phong;
            gui.ThuePhong thuePhong = new ThuePhong(sophong);
            thuePhong.Show();
        }

        private void ThanhToan_Click(object sender, RoutedEventArgs e)
        {
            var collection = listview_DMPhong.SelectedItems;
            List<string> LstSoPhong = new List<string>();
            for (int i = 0; i < collection.Count; i++)
            {
                int index = listview_DMPhong.Items.IndexOf(collection[i]);
                ThongTinPhong thongTin = DanhSachPhongTest[index];
                if (thongTin.TinhTrang == ThongTinPhong.TinhTrangPhong.ConTrong)
                    continue;
                LstSoPhong.Add(DanhSachPhongTest[index].Phong);
            }
            if(LstSoPhong.Count<1)
            {
                MessageBox.Show("Phòng trống");
                return;
            }
            var grid = this.Parent as Grid;
            grid.Children.Clear();
            UC_ThanhToan thanhToan = new UC_ThanhToan(LstSoPhong);
            grid.Children.Add(thanhToan);
        }
    }
}
