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
    /// Interaction logic for AddCustomerTypeWindow.xaml
    /// </summary>
    public partial class AddCustomerTypeWindow : Window
    {
        public AddCustomerTypeWindow()
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

        private void BtnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerType.InsertCustomerType(txbCustomerType.Text, System.Convert.ToDouble(txbSurcharge.Text), txbNote.Text))
                MessageBox.Show("Thêm thành công!");
            else
                MessageBox.Show("Thêm không thành công! Vui lòng kiểm tra lại thông tin đã nhập!");
        }

        private void TxbSurcharge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.RegulationUC.ListCustomerType.Clear();

            List<CustomerType> customerTypes = new List<CustomerType>();
            customerTypes.Clear();
            customerTypes.AddRange(CustomerType.GetCustomerType());
            foreach (var item in customerTypes)
                HotelManager.gui.RegulationUC.ListCustomerType.Add(item);

            CollectionViewSource.GetDefaultView(HotelManager.gui.RegulationUC.ListCustomerType).Refresh();
        }
    }
}
