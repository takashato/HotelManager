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
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        private Room _roomToEdit;

        public EditRoomWindow(Room roomToEdit)
        {
            InitializeComponent();

            _roomToEdit = roomToEdit;
            txbOldRoomName.Text = _roomToEdit.Name;
            txbOldRoomType.Text = _roomToEdit.Type;
            txbOldPrice.Text = _roomToEdit.PriceStr;
            txbOldNote.Text = _roomToEdit.Note;
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _roomToEdit.Name = txbNewRoomName.Text;
            _roomToEdit.Type = cbNewRoomType.Text;
            //_phongMuonSua.DonGia = Convert.ToDecimal( txbDonGiaMoi.Text );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbNewRoomType.ItemsSource = RoomType.GetRoomType();
            cbNewRoomType.DisplayMemberPath = "Type";
            cbNewRoomType.SelectedValuePath = "Price";
        }

        private string PriceStr => string.Format("{0:N0}", cbNewRoomType.SelectedValue);

        private void CbNewRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbNewPrice.Text = PriceStr;
        }
    }
}
