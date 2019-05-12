using HotelManager.db.model;
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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for ChangeRegulationUC.xaml
    /// </summary>
    public partial class RegulationUC : UserControl
    {
        public ObservableCollection<RoomType> ListRoomType { get; set; } = new ObservableCollection<RoomType>();
        public ObservableCollection<CustomerType> ListCustomerType { get; set; } = new ObservableCollection<CustomerType>();
        public RegulationUC()
        {
            InitializeComponent();

            //ListRoomType.Add(new RoomType() { Type = "VIP_PRO", Price = 7000000M, Note = "Phòng Vip" });
            //ListRoomType.Add(new RoomType() { Type = "VIPKUTE", Price = 6500000M, Note = "Phòng Xém Vip" });
            //ListRoomType.Add(new RoomType() { Type = "SIEUVIP", Price = 2300000M, Note = "Phòng Hơi Cùi" });
            //ListRoomType.Add(new RoomType() { Type = "VIP_VIP", Price = 1100000M, Note = "Phong Xập xệ" });
            //ListRoomType.Add(new RoomType() { Type = "ABCXYZH", Price = 6900000M, Note = "Phong Thoải Mái" });
            LoadListRoomTypeFromDB();
            LoadListCustomerTypeFromDB();
        }

        public void LoadListRoomTypeFromDB()
        {
            List<RoomType> roomType = new List<RoomType>();

            roomType.Clear();
            roomType.AddRange(RoomType.GetRoomType());

            ListRoomType.Clear();
            foreach (var item in roomType)
                ListRoomType.Add(item);
        }

        public void LoadListCustomerTypeFromDB()
        {
            List<CustomerType> customerType = new List<CustomerType>();

            customerType.Clear();
            customerType.AddRange(CustomerType.GetCustomerType());

            ListCustomerType.Clear();
            foreach (var item in customerType)
                ListCustomerType.Add(item);
        }

        private void AddRoomType_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomType roomTypeToDelete = dataGridListRoomType.SelectedItem as RoomType;
                var userAnswer = MessageBox.Show("Bạn có chắc muốn xóa loại phòng " + roomTypeToDelete.Type + " không? Thao tác sẽ không được hoàn lại.",
                                                 "Warning",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);
                if (userAnswer == MessageBoxResult.Yes)
                {
                    if (RoomType.DeleteRoomType(roomTypeToDelete.Type))
                    {
                        ListRoomType.Remove(roomTypeToDelete);
                        CollectionViewSource.GetDefaultView(ListRoomType).Refresh();
                        MessageBox.Show("Xóa loại phòng thành công!");
                    }
                    else
                        MessageBox.Show("Xóa loại phòng thất bại!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chưa chọn mục cần xóa hoặc mục được chọn trống!",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void EditRoomType_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void AddGuestType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGuestType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerType customerTypeToDelete = dataGridListCustomerType.SelectedItem as CustomerType;
                var userAnswer = MessageBox.Show("Bạn có chắc muốn xóa loại khách hàng " + customerTypeToDelete.Type + " không? Thao tác sẽ không được hoàn lại.",
                                                 "Warning",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);
                if (userAnswer == MessageBoxResult.Yes)
                {
                    if (CustomerType.DeleteCustomerType(customerTypeToDelete.Type))
                    {
                        ListCustomerType.Remove(customerTypeToDelete);
                        CollectionViewSource.GetDefaultView(ListCustomerType).Refresh();
                        MessageBox.Show("Xóa loại khách hàng thành công!");
                    }
                    else
                        MessageBox.Show("Xóa loại khách hàng thất bại!");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Chưa chọn mục cần xóa hoặc mục được chọn trống!", 
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void EditGuestType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
