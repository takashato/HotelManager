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
    /// Interaction logic for MassPaymentWindow.xaml
    /// </summary>
    public partial class MassPaymentWindow : Window
    {
        public MassPaymentWindow()
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

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbCustomerName.ItemsSource = Customer.GetCustomers();
            cbCustomerName.DisplayMemberPath = "Name";
            cbCustomerName.SelectedValuePath = "Address";
        }

        private void CbCustomerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbAddress.Text = (string)cbCustomerName.SelectedValue;
        }
    }
}
