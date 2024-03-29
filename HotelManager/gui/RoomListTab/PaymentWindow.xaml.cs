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
        private double totalMoney;
        private TimeSpan daysRent;
        public PaymentWindow(Room roomToPay)
        {
            InitializeComponent();

            _roomToPay = roomToPay;

            DateTime dateRent = new DateTime();
            dateRent = RentInfo.GetDateCheckin(_roomToPay.Name);
            DateTime datePay = new DateTime();
            datePay = DateTime.Now;

            daysRent = datePay.Subtract(dateRent);

            double roomPrice = (float)_roomToPay.Price * (daysRent.Days + 1); // Tiền phòng gốc theo ngày thuê (chưa tính phụ thu).
            int quantum = 0;
            if (RoomRentalDetail.GetQuantumCustomerInRoom(_roomToPay.Name) > RoomType.GetMaxCustomerInRoom(_roomToPay.Type))
                quantum = RoomRentalDetail.GetQuantumCustomerInRoom(_roomToPay.Name) - RoomType.GetMaxCustomerInRoom(_roomToPay.Type); // Số lượng khách vượt quá số khách tối đa theo quy định của loại phòng đó.
            double surchargeCustomerType = RoomRentalDetail.GetSurchargeCustomerInRoom(_roomToPay.Name) / 100; // Phụ thu theo loại khách trong phòng.
            double surchargeQuantumCustomer = CustomerSurcharge.GetSurchargeByQuantum(quantum) / 100; // Phụ thu theo số khách vượt quá số khách tối đa theo quy định của loại phòng đó.
            totalMoney = roomPrice + roomPrice * surchargeCustomerType + roomPrice * surchargeQuantumCustomer;

            txbRoomName.Text = roomToPay.Name;
            txbRoomType.Text = roomToPay.Type;
            txbDateRented.Text = dateRent.ToString("dd/MM/yyyy");
            txbDaysRented.Text = (daysRent.Days + 1).ToString();
            txbNumberGuests.Text = "" + RoomRentalDetail.GetQuantumCustomerInRoom(_roomToPay.Name);
            txbIsForeigns.Text = "" + RoomRentalDetail.GetQuantumForeignCustomerInRoom(_roomToPay.Name);
            txbTotalMoney.Text = "" + string.Format("{0:N0}", totalMoney);
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

            if (RentInfo.UpdateChechoutDate(_roomToPay.Name) && RoomRentalDetail.DeleteRoomRentalDetail(_roomToPay.Name) 
                && Room.UpdateRoomStatus(_roomToPay.Name) && RevenueReport.InsertRevenueReport(_roomToPay.Name, _roomToPay.Type, RentInfo.GetDateCheckin(_roomToPay.Name), DateTime.Now, totalMoney)
                && PaymentDetail.UpdatePaymentDetail(_roomToPay.Name, daysRent.Days + 1, totalMoney))
            {
                MessageBox.Show("Thanh toán thành công!");
            }
            else
                MessageBox.Show("Thanh toán không thành công! Vui lòng thực hiện lại thao tác!");
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            rooms.Clear();
            rooms.AddRange(Room.GetAll());

            HotelManager.gui.RoomListUC.RoomList.Clear();
            foreach (var item in rooms)
            {
                item.RetrieveRentInfo();
                HotelManager.gui.RoomListUC.RoomList.Add(item);
            }
            CollectionViewSource.GetDefaultView(HotelManager.gui.RoomListUC.RoomList).Refresh();
        }
    }
}
