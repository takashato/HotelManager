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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for RoomtypeStatisticUC.xaml
    /// </summary>
    public partial class RoomtypeStatisticUC : UserControl
    {
        public List<TestData> ListTest { get; set; } = new List<TestData>();

        public RoomtypeStatisticUC()
        {
            InitializeComponent();

            ListTest.Add(new TestData() { Roomtype = "Phòng VIP", Revenue = 1000000M });
            ListTest.Add(new TestData() { Roomtype = "Phòng CÙi", Revenue = 2000000M });
            ListTest.Add(new TestData() { Roomtype = "Phòng Thường", Revenue = 1500000M });
            ListTest.Add(new TestData() { Roomtype = "CŨng ngon Đếi", Revenue = 2400000M });
        }
    }
    public class TestData
    {
        public string Roomtype { get; set; }
        public decimal Revenue { get; set; }
    }
}
