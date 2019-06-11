﻿using HotelManager.db.model;
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
    /// Interaction logic for RoomtypeStatisticUC.xaml
    /// </summary>
    public partial class RoomtypeStatisticUC : UserControl
    {
        public static ObservableCollection<RevenueReport> RoomTypeReports { get; set; } = new ObservableCollection<RevenueReport>();

        public RoomtypeStatisticUC(DateTime StartDay, DateTime EndDay)
        {
            InitializeComponent();

            List<RevenueReport> reports = new List<RevenueReport>();
            reports.Clear();
            reports.AddRange(RevenueReport.GetAllByDate(StartDay, EndDay));

            RoomTypeReports.Clear();
            foreach (var item in reports)
                RoomTypeReports.Add(item);
        }
    }
}
