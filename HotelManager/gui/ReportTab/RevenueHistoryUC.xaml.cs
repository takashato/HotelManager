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
    /// Interaction logic for RevenueHistoryUC.xaml
    /// </summary>
    public partial class RevenueHistoryUC : UserControl
    {
        public static ObservableCollection<RevenueReport> RevenueReports { get; set; } = new ObservableCollection<RevenueReport>();

        public RevenueHistoryUC(DateTime StartDay, DateTime EndDay)
        {
            InitializeComponent();

            //Load RevenueReport from database
            List<RevenueReport> reports = new List<RevenueReport>();
            reports.Clear();
            reports.AddRange(RevenueReport.GetAllByDate(StartDay, EndDay));

            RevenueReports.Clear();
            foreach (var item in reports)
                RevenueReports.Add(item);

            //TestList.Add(new TestData() { RoomName = "B1.31", RoomType = "B-VIP       ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "C1001", RoomType = "C-VIP       ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "E12.1", RoomType = "E-Luxury    ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "A1.31", RoomType = "Phòng thường", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1.31", RoomType = "B-VIP       ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1331", RoomType = "B-VIP       ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "C1031", RoomType = "B-Luxury    ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "E1.31", RoomType = "E-Luxury    ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "A1331", RoomType = "Phòng thường", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "H1331", RoomType = "Phòng thường", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1A11", RoomType = "Phòng thường", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1H11", RoomType = "B-Luxury    ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1B11", RoomType = "B-Luxur     ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1B11", RoomType = "B-Luxur     ", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
            //TestList.Add(new TestData() { RoomName = "B1.31", RoomType = "Phòng thường", DateStarted = DateTime.Now, DatePaid = DateTime.Now, PaidAmount = 3000000M });
        }

        
            
        
        //public class TestData
        //{
        //    public string RoomName { get; set; }
        //    public string RoomType { get; set; }
        //    public DateTime DateStarted { get; set; }
        //    public DateTime DatePaid { get; set; }
        //    public decimal PaidAmount { get; set; }
        //    public string PaidAmountStr => string.Format("{0:N0}", PaidAmount);
        //}
    }
}
