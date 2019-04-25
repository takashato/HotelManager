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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for AccountUC.xaml
    /// </summary>
    public partial class AccountUC : UserControl
    {
        public ObservableCollection<Account> ListAccount { get; set; } = new ObservableCollection<Account>();
        public AccountUC()
        {
            InitializeComponent();

            ListAccount.Add(new Account() { Username = "taikhoan001", Name = "Nguyễn Huỳnh Lợi", AccountType = "Quản trị viên", DateCreated = DateTime.Now});
            ListAccount.Add(new Account() { Username = "taikhoan002", Name = "Đào Mạnh Dũng   ", AccountType = "Nhân viên    ", DateCreated = DateTime.Now});
            ListAccount.Add(new Account() { Username = "taikhoan003", Name = "Phạm Trần Chính ", AccountType = "Quản trị viên", DateCreated = DateTime.Now});
            ListAccount.Add(new Account() { Username = "taikhoan004", Name = "Bành Thanh Sơn  ", AccountType = "Quản lý      ", DateCreated = DateTime.Now});
        }

        public class Account
        {
            public string Username { get; set; }
            public string Name { get; set; }
            public string AccountType { get; set; }
            public DateTime DateCreated { get; set; }

        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            (new CreateAccountWindow()).ShowDialog();
        }

        private void changeAccount_Click(object sender, RoutedEventArgs e)
        {
            (new ChangeAccountWindow()).ShowDialog();
        }

        private void deleteAccount_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
