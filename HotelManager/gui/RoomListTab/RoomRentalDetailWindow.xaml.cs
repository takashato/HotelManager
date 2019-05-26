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
using System.Windows.Shapes;
using HotelManager.db.model;

namespace HotelManager.gui.RoomListTab
{
    /// <summary>
    /// Interaction logic for RoomRentalDetailWindow.xaml
    /// </summary>
    public partial class RoomRentalDetailWindow : Window
    {
        public static ObservableCollection<RoomRentalDetail> roomRentalDetails { get; set; } = new ObservableCollection<RoomRentalDetail>();
        private Room _roomToShowDetail;
        public RoomRentalDetailWindow(Room roomToShowDetail)
        {
            InitializeComponent();
            
            _roomToShowDetail = roomToShowDetail;
            LoadFromDB(_roomToShowDetail.Name);
            
            txbRoomName.Text = "Phòng " + _roomToShowDetail.Name;
            
        }

        public void LoadFromDB(string roomName)
        {
            List<RoomRentalDetail> roomRentalDetail = new List<RoomRentalDetail>();

            roomRentalDetail.Clear();
            roomRentalDetail.AddRange(RoomRentalDetail.GetRentalDetailByRoomName(roomName));

            roomRentalDetails.Clear();
            foreach (var item in roomRentalDetail)
                roomRentalDetails.Add(item);
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridCustomer_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
