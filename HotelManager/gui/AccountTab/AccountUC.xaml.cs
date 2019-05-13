using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelManager.db.model;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for AccountUC.xaml
    /// </summary>
    public partial class AccountUC : UserControl
    {
        public static ObservableCollection<Staff> ListAccount { get; set; } = new ObservableCollection<Staff>();
        public AccountUC()
        {
            InitializeComponent();

            //ListAccount.Add(new Account() { Username = "taikhoan001", Name = "Nguyễn Huỳnh Lợi", AccountType = "Quản trị viên", DateCreated = DateTime.Now});
            //ListAccount.Add(new Account() { Username = "taikhoan002", Name = "Đào Mạnh Dũng   ", AccountType = "Nhân viên    ", DateCreated = DateTime.Now});
            //ListAccount.Add(new Account() { Username = "taikhoan003", Name = "Phạm Trần Chính ", AccountType = "Quản trị viên", DateCreated = DateTime.Now});
            //ListAccount.Add(new Account() { Username = "taikhoan004", Name = "Bành Thanh Sơn  ", AccountType = "Quản lý      ", DateCreated = DateTime.Now});
            LoadListAccountFromDB();
        }

        //public class Account
        //{
        //    public string Username { get; set; }
        //    public string Name { get; set; }
        //    public string AccountType { get; set; }
        //    public DateTime DateCreated { get; set; }

        //}

        public void LoadListAccountFromDB()
        {
            List<Staff> account = new List<Staff>();
            account.Clear();
            account.AddRange(Staff.GetAll());

            ListAccount.Clear();
            foreach (var item in account)
                ListAccount.Add(item);
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            (new CreateAccountWindow()).ShowDialog();
        }

        private void changeAccount_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridListAccount.SelectedIndex < 0)
                return;
            Staff accountToChange = dataGridListAccount.SelectedItem as Staff;
            (new ChangeAccountWindow(accountToChange)).ShowDialog();
        }

        private void deleteAccount_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
