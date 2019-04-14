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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for BaoCao.xaml
    /// </summary>
    public partial class BaoCao : UserControl
    {
        ObservableCollection<BanBaoCao> BaoCaos = new ObservableCollection<BanBaoCao>();
        public BaoCao()
        {
            InitializeComponent();
            TableBaoCao.ItemsSource = BaoCaos;
        }
        private class BanBaoCao
        {
            string LoaiPhong { get; set; }
            ulong DoanhThu { get; set; }
            string TiLe { get; set; }
        }
    }
}
