using HotelManager.db.model;
using HotelManager.gui.RegulationTab;
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
        public static ObservableCollection<RoomType> ListRoomType { get; set; } = new ObservableCollection<RoomType>();
        public static ObservableCollection<CustomerType> ListCustomerType { get; set; } = new ObservableCollection<CustomerType>();
        public static ObservableCollection<CustomerSurcharge> ListCustomerSurcharge { get; set; } = new ObservableCollection<CustomerSurcharge>();
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
            LoadListCustomerSurchargeFromDB();
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

        public void LoadListCustomerSurchargeFromDB()
        {
            List<CustomerSurcharge> customerSurcharge = new List<CustomerSurcharge>();

            customerSurcharge.Clear();
            customerSurcharge.AddRange(CustomerSurcharge.GetAll());

            ListCustomerSurcharge.Clear();
            foreach (var item in customerSurcharge)
                ListCustomerSurcharge.Add(item);
        }

        private void AddRoomType_Click(object sender, RoutedEventArgs e)
        {
            (new AddRoomTypeWindow()).ShowDialog();
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
            if (dataGridListRoomType.SelectedIndex < 0)
                return;
            RoomType roomTypeToEdit = dataGridListRoomType.SelectedItem as RoomType;
            (new EditRoomTypeWindow(roomTypeToEdit)).ShowDialog();
        }

        private void AddGuestType_Click(object sender, RoutedEventArgs e)
        {
            (new AddCustomerTypeWindow()).ShowDialog();
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
            if (dataGridListCustomerType.SelectedIndex < 0)
                return;
            CustomerType customerTypeToEdit = dataGridListCustomerType.SelectedItem as CustomerType;
            (new EditCustomerTypeWindow(customerTypeToEdit)).ShowDialog();
        }

        private void AddGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {
            (new AddCustomerSurchargeWindow()).ShowDialog();
        }

        private void DeleteGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerSurcharge customerSurchargeToDelete = dataGridListCustomerSurcharge.SelectedItem as CustomerSurcharge;
                var userAnswer = MessageBox.Show("Bạn có chắc muốn xóa loại phụ thu khách hàng " + customerSurchargeToDelete.Quantum + " không? Thao tác sẽ không được hoàn lại.",
                                                 "Warning",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);
                if (userAnswer == MessageBoxResult.Yes)
                {
                    if (CustomerSurcharge.DeleteCustomerSurcharge(customerSurchargeToDelete.Quantum))
                    {
                        ListCustomerSurcharge.Remove(customerSurchargeToDelete);
                        CollectionViewSource.GetDefaultView(ListCustomerSurcharge).Refresh();
                        MessageBox.Show("Xóa loại khách hàng thành công!");
                    }
                    else
                        MessageBox.Show("Xóa loại khách hàng thất bại!");
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

        private void EditGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridListCustomerSurcharge.SelectedIndex < 0)
                return;
            CustomerSurcharge customerSurchargeToEdit = dataGridListCustomerSurcharge.SelectedItem as CustomerSurcharge;
            (new EditCustomerSurchargeWindow(customerSurchargeToEdit)).ShowDialog();
        }
    }
}
