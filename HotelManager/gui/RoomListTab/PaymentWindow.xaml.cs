﻿using System;
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

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private Room _roomToPay;
        public PaymentWindow(Room roomToPay)
        {
            InitializeComponent();

            _roomToPay = roomToPay;
            txbRoomName.Text = roomToPay.Name;
            txbRoomType.Text = roomToPay.Type;
            txbDateRented.Text = "";
            txbDaysRented.Text = "";
            txbNumberGuests.Text = "";
            txbIsForeigns.Text = "";
            txbTotalMoney.Text = "";
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            _roomToPay.Status = Room.EStatus.Available;
            this.Close();
        }
    }
}