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
    /// Interaction logic for EditCustomerTypeWindow.xaml
    /// </summary>
    public partial class EditCustomerTypeWindow : Window
    {
        private CustomerType customerType;
        public EditCustomerTypeWindow(CustomerType _customerType)
        {
            InitializeComponent();
            customerType = _customerType;
            txbNewCustomerType.Text = customerType.Type;
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
            if ("".Equals(txbNewCustomerType.Text) || "".Equals(txbNewSurcharge.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
            else
            {
                if (CustomerType.UpdateCustomerType(txbNewCustomerType.Text, System.Convert.ToDouble(txbNewSurcharge.Text), txbNewNote.Text, customerType.Type))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    this.Close();
                }
                else
                    MessageBox.Show("Cập nhật không thành công! Vui lòng kiểm tra lại thông tin! (Loại khách hàng và phụ thu không được trùng với loại khách hàng và phụ thu đã có)");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.RegulationUC.ListCustomerType.Clear();

            List<CustomerType> customerTypes = new List<CustomerType>();
            customerTypes.Clear();
            customerTypes.AddRange(CustomerType.GetCustomerType());
            foreach (var item in customerTypes)
                HotelManager.gui.RegulationUC.ListCustomerType.Add(item);

            CollectionViewSource.GetDefaultView(RegulationUC.ListCustomerType).Refresh();
        }
    }
}
