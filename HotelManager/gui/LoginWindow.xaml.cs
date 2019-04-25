using HotelManager.db;
using Dapper;
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
using MaterialDesignThemes.Wpf;
using System.Threading;
using HotelManager.gui.dialog;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool IsDialogOpened { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            tbUsername.Text = Properties.Settings.Default.sUsername;
            pbPassword.Password = Properties.Settings.Default.sPassword;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_DangNhap_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.Show(new LoadingDialog());

            string username = tbUsername.Text;
            string password = pbPassword.Password;

            Staff staff = Staff.GetByUsername(username);
            if (staff == null)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                DialogHost.Show(new MessageDialog("Tài khoản không tồn tại!"));
            }
            else if (!BCrypt.Net.BCrypt.Verify(password, staff.Password))
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                DialogHost.Show(new MessageDialog("Mật khẩu không chính xác!"));
            }
            else
            {
                // Store session
                App.Instance._Session = new data.Session(staff);

                // Store password
                if (ckbSave.IsChecked == true)
                {
                    Properties.Settings.Default.sUsername = username;
                    Properties.Settings.Default.sPassword = password;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.sUsername = "";
                    Properties.Settings.Default.sPassword = "";
                    Properties.Settings.Default.Save();
                }

                // Show form
                this.Hide();
                App.Instance._MainWindow.Show();
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
