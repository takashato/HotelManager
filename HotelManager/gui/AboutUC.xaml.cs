using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AboutUC.xaml
    /// </summary>
    public partial class AboutUC : UserControl
    {
        private string _linkProject = "https://github.com/takashato/HotelManager";
        private Dictionary<string, string> _linkMembers = new Dictionary<string, string>();

        public AboutUC()
        {
            InitializeComponent();

            _linkMembers.Add(btnBTS.Tag as string, "https://github.com/takashato");
            _linkMembers.Add(btnPTC.Tag as string, "https://github.com/PTChinh");
            _linkMembers.Add(btnNHL.Tag as string, "https://github.com/loia5tqd001");
            _linkMembers.Add(btnDMD.Tag as string, "https://github.com/manhdung99");
        }

        private void GoToGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_linkProject);
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromArgb(200, 0, 184, 148));
            (sender as Button).Foreground = Brushes.White;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
            (sender as Button).Foreground = new SolidColorBrush(Color.FromRgb(0, 184, 148));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string linkToStart = _linkMembers[(sender as Button).Tag as string];

            if (!string.IsNullOrEmpty(linkToStart))
                Process.Start(linkToStart);
        }
    }
}
