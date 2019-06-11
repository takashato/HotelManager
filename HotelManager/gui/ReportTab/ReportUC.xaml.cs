using HotelManager.db.model;
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
    /// Interaction logic for ReportUC.xaml
    /// </summary>
    public partial class ReportUC : UserControl
    {
        public ReportUC()
        {
            InitializeComponent();
            GrdContent.Children.Add(new RevenueHistoryUC((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
            txbTotalRevenue.Text = string.Format("{0:N0}", RevenueReport.GetTotalRevenueByDate((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTag = (cbbReportType.SelectedItem as ComboBoxItem).Tag as string;
            if (selectedTag == "RevenueHistory")
            {
                GrdContent?.Children.Clear();
                GrdContent?.Children.Add(new RevenueHistoryUC((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
            }
            else if (selectedTag == "RoomtypeStatistic")
            {
                GrdContent?.Children?.Clear();
                GrdContent.Children.Add(new RoomtypeStatisticUC((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
            }

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txbTotalRevenue.Text = string.Format("{0:N0}", RevenueReport.GetTotalRevenueByDate((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
                string selectedTag = (cbbReportType.SelectedItem as ComboBoxItem).Tag as string;
                if (selectedTag == "RevenueHistory")
                {
                    GrdContent?.Children.Clear();
                    GrdContent?.Children.Add(new RevenueHistoryUC((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
                }
                else if (selectedTag == "RoomtypeStatistic")
                {
                    GrdContent?.Children?.Clear();
                    GrdContent.Children.Add(new RoomtypeStatisticUC((DateTime)dtpStartDay.SelectedDate, (DateTime)dtpEndDay.SelectedDate));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
