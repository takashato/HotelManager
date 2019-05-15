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
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
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
            cbAccountType.ItemsSource = StaffType.GetStaffTypes();
            cbAccountType.SelectedIndex = 0;
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if ("".Equals(txbUsername.Text) || "".Equals(txbPassword.Password) || "".Equals(txbRepassword.Password) || "".Equals(txbFullName.Text) || "".Equals(cbAccountType.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            if(txbPassword.Password.Equals(txbRepassword.Password))
            {
                if (Staff.InsertStaff(txbUsername.Text, txbPassword.Password, txbFullName.Text, cbAccountType.Text))
                    MessageBox.Show("Tạo tài khoản thành công!");
                else
                    MessageBox.Show("Tạo tài khoản không thành công!");
            }
            else
            {
                MessageBox.Show("Nhập lại mật khẩu không khớp!");
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotelManager.gui.AccountUC.ListAccount.Clear();
            List<Staff> account = new List<Staff>();
            account.Clear();
            account.AddRange(Staff.GetAll());

            HotelManager.gui.AccountUC.ListAccount.Clear();
            foreach (var item in account)
                HotelManager.gui.AccountUC.ListAccount.Add(item);
            CollectionViewSource.GetDefaultView(HotelManager.gui.AccountUC.ListAccount).Refresh();
        }
    }
}
