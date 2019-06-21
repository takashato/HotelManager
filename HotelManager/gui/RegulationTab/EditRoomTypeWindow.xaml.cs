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
            if ("".Equals(txbNewRoomType.Text) || "".Equals(txbNewPrice.Text) || "".Equals(cbNewMaxCustomer.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
            else
            {
                if (RoomType.UpdateRoomType(txbNewRoomType.Text, System.Convert.ToDecimal(txbNewPrice.Text), Convert.ToInt32(cbNewMaxCustomer.Text), txbNewNote.Text, roomTypeToEdit.Type))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    this.Close();
                }
                else
                    MessageBox.Show("Cập nhật không thành công! Vui lòng kiểm tra lại thông tin! (Loại phòng và đơn giá không được trùng với loại phòng và đơn giá đã có)");
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 2; i < 8; i++)
                cbNewMaxCustomer.Items.Add(i + 1);
        }
    }
}
