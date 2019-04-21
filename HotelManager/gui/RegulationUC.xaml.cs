using HotelManager.db.model;
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
    /// Interaction logic for ChangeRegulationUC.xaml
    /// </summary>
    public partial class RegulationUC : UserControl
    {
        public ObservableCollection<RoomType> ListRoomType { get; set; } = new ObservableCollection<RoomType>();
        public RegulationUC()
        {
            InitializeComponent();

            ListRoomType.Add(new RoomType() { Type = "VIP_PRO", Price = 7000000M, Note = "Phòng Vip" });
            ListRoomType.Add(new RoomType() { Type = "VIPKUTE", Price = 6500000M, Note = "Phòng Xém Vip" });
            ListRoomType.Add(new RoomType() { Type = "SIEUVIP", Price = 2300000M, Note = "Phòng Hơi Cùi" });
            ListRoomType.Add(new RoomType() { Type = "VIP_VIP", Price = 1100000M, Note = "Phong Xập xệ" });
            ListRoomType.Add(new RoomType() { Type = "ABCXYZH", Price = 6900000M, Note = "Phong Thoải Mái" });
        }

        private void AddRoomType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRoomType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditRoomType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddGuestType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGuestType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditGuestType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditGuestSurcharge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
