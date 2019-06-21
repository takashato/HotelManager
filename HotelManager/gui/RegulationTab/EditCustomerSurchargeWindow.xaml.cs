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
    /// Interaction logic for EditCustomerSurchargeWindow.xaml
    /// </summary>
    public partial class EditCustomerSurchargeWindow : Window
    {
        private CustomerSurcharge customerSurchargeToEdit;
        public EditCustomerSurchargeWindow(CustomerSurcharge _customerSurchargeToEdit)
        {
            InitializeComponent();
            customerSurchargeToEdit = _customerSurchargeToEdit;
            cbNewQuantum.SelectedIndex = customerSurchargeToEdit.Quantum - 1;
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

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ("".Equals(txbNewSurcharge.Text) || "".Equals(cbNewQuantum.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
            else
            {
                if (CustomerSurcharge.UpdateCustomerSurcharge(cbNewQuantum.SelectedIndex + 1, System.Convert.ToDouble(txbNewSurcharge.Text), txbNewNote.Text, customerSurchargeToEdit.Quantum))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    this.Close();
                }
                else
                    MessageBox.Show("Cập nhật không thành công! Vui lòng kiểm tra lại thông tin! (Số khách và phụ thu không được trùng số khách và phụ thu đã có)");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
                cbNewQuantum.Items.Add(i + 1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.RegulationUC.ListCustomerSurcharge.Clear();

            List<CustomerSurcharge> customerSurcharges = new List<CustomerSurcharge>();
            customerSurcharges.Clear();
            customerSurcharges.AddRange(CustomerSurcharge.GetAll());
            foreach (var item in customerSurcharges)
                HotelManager.gui.RegulationUC.ListCustomerSurcharge.Add(item);

            CollectionViewSource.GetDefaultView(RegulationUC.ListCustomerSurcharge).Refresh();
        }
    }
}
