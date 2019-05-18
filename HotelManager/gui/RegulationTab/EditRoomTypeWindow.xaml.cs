using HotelManager.db.model;
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

namespace HotelManager.gui.RegulationTab
{
    /// <summary>
    /// Interaction logic for EditRoomTypeWindow.xaml
    /// </summary>
    public partial class EditRoomTypeWindow : Window
    {
        private RoomType roomTypeToEdit;
        public EditRoomTypeWindow(RoomType _roomTypeToEdit)
        {
            InitializeComponent();
            roomTypeToEdit = _roomTypeToEdit;
            txbNewRoomType.Text = roomTypeToEdit.Type;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomType.UpdateRoomType(txbNewRoomType.Text, System.Convert.ToDecimal(txbNewPrice.Text), txbNewNote.Text, roomTypeToEdit.Type))
                MessageBox.Show("Cập nhật thành công!");
            else
                MessageBox.Show("Cập nhật không thành công!");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.RegulationUC.ListRoomType.Clear();

            List<RoomType> roomTypes = new List<RoomType>();
            roomTypes.Clear();
            roomTypes.AddRange(RoomType.GetRoomType());
            foreach (var item in roomTypes)
                HotelManager.gui.RegulationUC.ListRoomType.Add(item);

            CollectionViewSource.GetDefaultView(RegulationUC.ListRoomType).Refresh();
        }
    }
}
