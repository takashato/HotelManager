using HotelManager.db.model;
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

        public List<Room> RoomList { get; set; } = new List<Room>();

        public DanhMucPhong()
        {
            InitializeComponent();
            DataContext = this;
            LoadFromDB();
        }

        public void LoadFromDB()
        {
            var roomList = Room.GetAll();
            RoomList.Clear();
            RoomList.AddRange(roomList);
        }

        private void ThuePhong_Click(object sender, RoutedEventArgs e)
        {
            //Truyền tham số tên phòng vào Form Thuê Phòng
            int index = listview_DMPhong.SelectedIndex;
            if (index < 0)
                return;
            string sophong = "Phòng " + RoomList[index].Name;
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
                Room thongTin = RoomList[index];
                if (thongTin.Status == Room.EStatus.Available)
                    continue;
                LstSoPhong.Add(RoomList[index].Name);
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
