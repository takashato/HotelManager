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
    /// Interaction logic for AddCustomerSurchargeWindow.xaml
    /// </summary>
    public partial class AddCustomerSurchargeWindow : Window
    {
        public AddCustomerSurchargeWindow()
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
            for (int i = 0; i < 5; i++)
                cbQuantum.Items.Add(i + 1);          
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

        private void BtnAddCustomerSurcharge_Click(object sender, RoutedEventArgs e)
        {
            if ("".Equals(txbSurcharge.Text) || "".Equals(cbQuantum.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
            else
            {
                if (CustomerSurcharge.InsertCustomerSurcharge(System.Convert.ToInt32(cbQuantum.Text), System.Convert.ToDouble(txbSurcharge.Text), txbNote.Text))
                {
                    MessageBox.Show("Thêm thành công!");
                    this.Close();
                }
                else
                    MessageBox.Show("Thêm không thành công! Vui lòng kiểm tra lại thông tin! (Số khách và phụ thu không được trùng số khách và phụ thu đã có)");
            }
        }
    }
}
