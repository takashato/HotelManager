using HotelManager.db.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddRoomTypeWindow.xaml
    /// </summary>
    public partial class AddRoomTypeWindow : Window
    {
        public AddRoomTypeWindow()
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

        private void BtnAddRoomType_Click(object sender, RoutedEventArgs e)
        {
            if ("".Equals(txbRoomType.Text) || "".Equals(txbPrice.Text) || "".Equals(cbMaxCustomer.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin có dấu * ");                
            }
            else
            {

                if (RoomType.InsertRoomType(txbRoomType.Text, System.Convert.ToDecimal(txbPrice.Text), txbNote.Text, Convert.ToInt32(cbMaxCustomer.Text)))
                {
                    MessageBox.Show("Thêm thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công! Vui lòng kiểm tra lại thông tin nhập! (Loại phòng và đơn giá không được trùng với loại phòng và đơn giá đã có)");
                }
            }
        }

        private void TxbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.RegulationUC.ListRoomType.Clear();

            List<RoomType> roomTypes = new List<RoomType>();
            roomTypes.Clear();
            roomTypes.AddRange(RoomType.GetRoomType());
            foreach (var item in roomTypes)
                HotelManager.gui.RegulationUC.ListRoomType.Add(item);

            CollectionViewSource.GetDefaultView(HotelManager.gui.RegulationUC.ListRoomType).Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 2; i < 8; i++)
                cbMaxCustomer.Items.Add(i + 1);
        }
    } 
}
