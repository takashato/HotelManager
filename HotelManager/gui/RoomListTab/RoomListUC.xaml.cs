using HotelManager.db.model;
using HotelManager.gui.RoomListTab;
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
    public partial class RoomListUC : UserControl
    {
        public static ObservableCollection<Room> RoomList { get; set; } = new ObservableCollection<Room>();

        public RoomListUC()
        {
            InitializeComponent();

            SetRoomListFilter();
        }

        private void SetRoomListFilter()
        {
            // Filtering list room by listview-filtering. Details here: https://www.wpf-tutorial.com/listview-control/listview-filtering/
            try
            {
                (CollectionViewSource.GetDefaultView(lsvRoomList.ItemsSource)).Filter = RoomListFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \"" + ex.Message + "\" caught in SetRoomListFilter() - DanhMucPhong.xaml.cs");
            }
        }

        private bool RoomListFilter(object item)
        {
            if (String.IsNullOrEmpty(txbSearchBar.Text))
                return true;
            else
                return ((item as Room).Name.IndexOf(txbSearchBar.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as Room).Type.IndexOf(txbSearchBar.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public async void LoadFromDB()
        {
            App.Instance._MainWindow.ShowViewLoading();
            var rooms = await Room.GetAllAsync();
            RoomList.Clear();
            foreach (var room in rooms)
            {
                room.RetrieveRentInfo();
                RoomList.Add(room);
            }
            App.Instance._MainWindow.CloseViewDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txbSearchBar.Visibility == Visibility.Hidden)
            {
                txbSearchBar.Visibility = Visibility.Visible;
            }
            else if (txbSearchBar.Visibility == Visibility.Visible)
            {
                txbSearchBar.Text = "";
                txbSearchBar.Visibility = Visibility.Hidden;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lsvRoomList.ItemsSource).Refresh();
        }

        // Codes symbolize only for testing UI purpose. Bugs still exist. Who's responsible need to rewrite this function.
        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            if (lsvRoomList.SelectedIndex < 0)
                return;

            Room roomToRent = lsvRoomList.SelectedItem as Room;

            if (!roomToRent.IsAvailable())
            {
                // TODO: Thay CustomMessageBox đẹp hơn
                MessageBox.Show("Phòng " + roomToRent.Name + " đã được thuê",
                    "Thuê phòng không thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            (new RentingWindow(roomToRent)).ShowDialog();
            // In the line above we would change AN ITEM of RoomList ObservableCollection but NOT THE RoomList COLLECTION ITSELF
            // So we need the line below to refresh the RoomList Collection to raise the change on binding
            // More details in: https://nishantrana.me/2012/04/12/refresh-observablecollection-in-wpf/
            CollectionViewSource.GetDefaultView(RoomList).Refresh();
        }

        // Codes symbolize only for testing UI purpose. Bugs still exist. Who's responsible need to rewrite this function.
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            if (lsvRoomList.SelectedIndex < 0)
                return;

            Room roomToPay = lsvRoomList.SelectedItem as Room;

            if (roomToPay.Status == Room.EStatus.Available)
            {
                MessageBox.Show("Phòng " + roomToPay.Name + " chưa được thuê",
                    "Thanh toán không thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            new PaymentWindow(roomToPay).ShowDialog();
            CollectionViewSource.GetDefaultView(RoomList).Refresh();
        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (App.Instance._Session.CurrentStaff.Level.ToString().Equals("Receptionist"))
                MessageBox.Show("Bạn không có quyền sửa thông tin phòng!",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            else
            {
                if (lsvRoomList.SelectedIndex < 0)
                    return;

                Room roomToEdit = lsvRoomList.SelectedItem as Room;

                if (roomToEdit.Status == Room.EStatus.NotAvailable)
                {
                    App.Instance._MainWindow.ShowMessage("Không thể sửa phòng đang được thuê!");
                    return;
                }

                var dialogResult = (new EditRoomWindow(roomToEdit)).ShowDialog();
                if (dialogResult == true)
                    LoadFromDB(); // Update form
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (App.Instance._Session.CurrentStaff.Level.ToString().Equals("Receptionist"))
                MessageBox.Show("Bạn không có quyền xóa phòng!",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            else
            {
                // TODO: Code bên dưới chỉ xóa được một phòng. 
                // Có thể nâng cấp lên để xóa nhiều phòng cùng một lúc khi người dùng Ctrl/Shift chọn nhiều phòng một lúc lúc bấm nút xóa
                if (lsvRoomList.SelectedIndex < 0)
                    return;

                Room roomToDelete = lsvRoomList.SelectedItem as Room;

                //if (roomToDelete.Status == Room.EStatus.NotAvailable)
                //{
                //    MessageBox.Show("Không thể xóa phòng đang được thuê",
                //        "Xóa phòng không thành công",
                //        MessageBoxButton.OK,
                //        MessageBoxImage.Error);
                //    return;
                //}
                //else
                //{
                //    var userAnswer = MessageBox.Show(
                //        "Bạn có chắc muốn xóa phòng " + roomToDelete.Name + " không? Thao tác sẽ không được hoàn lại.",
                //        "Cảnh báo",
                //        MessageBoxButton.YesNo,
                //        MessageBoxImage.Warning);

                //    if (userAnswer == MessageBoxResult.Yes)
                //    {
                //        RoomList.Remove(roomToDelete);
                //        CollectionViewSource.GetDefaultView(this.RoomList).Refresh();
                //        Room.DeleteRoom(roomToDelete.Name);
                //    }

                //}
                //int flag = 0;
                //string listRoomName = "";
                //foreach(Room item in roomToDelete)
                //{
                //    if (item.Status == Room.EStatus.NotAvailable)
                //        flag = 1;
                //    listRoomName += "<" + item.Name + "> ";
                //}

                if (roomToDelete.Status == Room.EStatus.NotAvailable)
                {
                    MessageBox.Show("Không thể xóa phòng đang được thuê",
                                    "Xóa phòng không thành công",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }
                else
                {
                    var userAnswer = MessageBox.Show("Bạn có chắc muốn xóa phòng " + roomToDelete.Name + " không? Thao tác sẽ không được hoàn lại.",
                                                     "Cảnh báo",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Warning);
                    if (userAnswer == MessageBoxResult.Yes)
                    {
                        if (Room.DeleteRoom(roomToDelete.Name))
                        {
                            //foreach(Room item in roomToDelete)
                            RoomList.Remove(roomToDelete);
                            CollectionViewSource.GetDefaultView(RoomList).Refresh();
                            //foreach(Room item in roomToDelete)

                            MessageBox.Show("Xóa phòng thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Xóa phòng thất bại!");
                        }
                    }
                }
            }
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (App.Instance._Session.CurrentStaff.Level.ToString().Equals("Receptionist"))
                MessageBox.Show("Bạn không có quyền thêm phòng!",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            else
                (new AddRoomWindow()).ShowDialog();
        }

        private void btnMassPayment_Click(object sender, RoutedEventArgs e)
        {
            (new MassPaymentWindow()).ShowDialog();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFromDB();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (lsvRoomList.SelectedIndex < 0)
                return;
            Room roomToShowDetails = lsvRoomList.SelectedItem as Room;

            if (roomToShowDetails.Status == Room.EStatus.Available)
            {
                MessageBox.Show("Phòng " + roomToShowDetails.Name + " chưa được thuê",
                    "Thanh toán không thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            else
            {
                (new RoomRentalDetailWindow(roomToShowDetails)).ShowDialog();
            }
        }
    }
}
