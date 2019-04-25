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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbRoomType.ItemsSource = RoomType.GetRoomType();
            cbRoomType.DisplayMemberPath = "Type";
            cbRoomType.SelectedValuePath = "Price";
        }

        private string PriceStr => string.Format("{0:N0}", cbRoomType.SelectedValue);

        private void CbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbPrice.Text = PriceStr;
        }

        private void BtnAddRoom_Click(object sender, RoutedEventArgs e)
        {

            if (Room.InsertRoom(txbRoomName.Text, cbRoomType.Text, txbNote.Text))
            {
                MessageBox.Show("Thêm thành công!");
            }
            else
            {
                MessageBox.Show("Thêm không thành công!\nTên phòng đã có");
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //HotelManager.gui.RoomListUC.RoomList.Clear();
            //HotelManager.gui.RoomListUC.RoomList.AddRange(Room.GetAll());
            //CollectionViewSource.GetDefaultView(HotelManager.gui.RoomListUC.RoomList).Refresh();
        }
    }
}
