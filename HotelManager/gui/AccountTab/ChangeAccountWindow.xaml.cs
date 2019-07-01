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
    /// Interaction logic for ChangeAccountWindow.xaml
    /// </summary>
    public partial class ChangeAccountWindow : Window
    {
        private Staff accountToChange;

        public ChangeAccountWindow()
        {
            InitializeComponent();
            txbUsername.Text = App.Instance._Session.CurrentStaff.Username;
            txbFullName.Text = App.Instance._Session.CurrentStaff.Fullname;
            txbFullName.IsEnabled = false;
            cbAccountType.SelectedIndex = (int)App.Instance._Session.CurrentStaff.Level - 1;
            cbAccountType.IsEnabled = false;
        }

        public ChangeAccountWindow(Staff _accountToChange)
        {
            InitializeComponent();
            accountToChange = _accountToChange;
            txbUsername.Text = accountToChange.Username;
            txbFullName.Text = accountToChange.Fullname;
            cbAccountType.SelectedIndex = (int)accountToChange.Level - 1;
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
            if ("".Equals(txbPassword.Password) || "".Equals(txbFullName.Text) || "".Equals(cbAccountType.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            if (Staff.UpdateStaff(txbPassword.Password, txbFullName.Text, cbAccountType.Text, txbUsername.Text))
                MessageBox.Show("Cập nhật thành công!");
            else
                MessageBox.Show("Cập nhật không thành công!");

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbAccountType.ItemsSource = StaffType.GetStaffTypes();
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

