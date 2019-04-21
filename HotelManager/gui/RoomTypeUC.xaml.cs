using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HotelManager.db.model;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for RoomTypeUC.xaml
    /// </summary>
    public partial class RoomTypeUC : UserControl
    {
        public ObservableCollection<RoomType> ListRoomType { get; set; } = new ObservableCollection<RoomType>();
        public RoomTypeUC()
        {
            InitializeComponent();

            ListRoomType.Add(new RoomType() { Type = "VIP_PRO", Price = 7000000M, Note = "Phòng Vip" });
            ListRoomType.Add(new RoomType() { Type = "VIPKUTE", Price = 6500000M, Note = "Phòng Xém Vip" });
            ListRoomType.Add(new RoomType() { Type = "SIEUVIP", Price = 2300000M, Note = "Phòng Hơi Cùi" });
            ListRoomType.Add(new RoomType() { Type = "VIP_VIP", Price = 1100000M, Note = "Phong Xập xệ" });
            ListRoomType.Add(new RoomType() { Type = "ABCXYZH", Price = 6900000M, Note = "Phong Thoải Mái" });
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            (new AddRoomTypeWindow()).ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsvListRoomType.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một chỉ mục để thực hiện thao tác", 
                    "Xóa loại phòng không thành công", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            RoomType roomTypeToDelete = lsvListRoomType.SelectedItem as RoomType;

            // TODO: write handle here
            bool ifAnyRoomRentingBelongToThisRoomType = false;

            if (ifAnyRoomRentingBelongToThisRoomType)
            {
                MessageBox.Show("Không thể xóa loại phòng " + roomTypeToDelete.Type + " vì có phòng loại này đang được thuê",
                    "Xóa loại phòng không thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            else
            {
                var userAnswer = MessageBox.Show(
                    "Bạn có chắc muốn xóa loại phòng " + roomTypeToDelete.Type + " ra khỏi danh sách không? Thao tác sẽ không được hoàn lại.",
                    "Cảnh báo",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (userAnswer == MessageBoxResult.Yes)
                    ListRoomType.Remove(roomTypeToDelete);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lsvListRoomType.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một chỉ mục để thực hiện thao tác",
                    "Chỉnh sửa thông tin loại phòng không thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            (new EditRoomTypeWindow()).ShowDialog();
        }
    }
}
