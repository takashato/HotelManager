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
using HotelManager.db.model;
using MaterialDesignThemes.Wpf;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        private Room _roomToEdit;

        public EditRoomWindow(Room roomToEdit)
        {
            InitializeComponent();

            _roomToEdit = roomToEdit;
            txbNewRoomName.Text = _roomToEdit.Name;
            txbNewNote.Text = Room.GetRoomNoteByName(_roomToEdit.Name);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Not changed
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //_roomToEdit.Name = txbNewRoomName.Text;
            //_roomToEdit.Type = cbNewRoomType.Text;
            //_phongMuonSua.DonGia = Convert.ToDecimal( txbDonGiaMoi.Text );
            bool result = Room.UpdateRoom(txbNewRoomName.Text, _roomToEdit.Name, (cbNewRoomType.SelectedItem as RoomType).Type, txbNewNote.Text);
            this.DialogResult = result;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbNewRoomType.ItemsSource = RoomType.GetRoomType();
            cbNewRoomType.DisplayMemberPath = "Type";
            cbNewRoomType.SelectedValuePath = "Price";
            foreach (RoomType roomType in cbNewRoomType.Items)
            {
                if (roomType.Type.Equals(_roomToEdit.Type))
                {
                    cbNewRoomType.SelectedItem = roomType;
                }
            }
        }

        private string GetPriceStr() 
        {
            if (cbNewRoomType.SelectedItem == null) return "";
            return string.Format("{0:N0}", (cbNewRoomType.SelectedItem as RoomType).Price);
        }

        private void CbNewRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbNewPrice.Text = GetPriceStr();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            rooms.Clear();
            rooms.AddRange(Room.GetAll());

            HotelManager.gui.RoomListUC.RoomList.Clear();
            foreach (var item in rooms)
            {
                item.RetrieveRentInfo();
                HotelManager.gui.RoomListUC.RoomList.Add(item);
            }
            CollectionViewSource.GetDefaultView(HotelManager.gui.RoomListUC.RoomList).Refresh();
        }
    }
}
