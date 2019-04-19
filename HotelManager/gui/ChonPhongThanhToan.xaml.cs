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

using System.Collections.ObjectModel;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for ChonPhongThanhToan.xaml
    /// </summary>
    public partial class ChonPhongThanhToan : Window
    {
        ObservableCollection<UC_ThanhToan.Phong> phongs;
        public ChonPhongThanhToan()
        {
            InitializeComponent();
        }

        public ChonPhongThanhToan(ObservableCollection<UC_ThanhToan.Phong> phongs)
        {
            InitializeComponent();
            this.phongs = phongs;
        }


        private void DanhMucPhong_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UC_ThanhToan.Phong phong = new UC_ThanhToan.Phong();
            phong.SoPhong = this.DMPhong.RoomList[this.DMPhong.listview_DMPhong.SelectedIndex].Name;
            phongs.Add(phong);
        }
    }
}
