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
        }
    }
}
