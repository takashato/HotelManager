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
using System.Windows.Shapes;
using HotelManager.db.model;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for MassPaymentWindow.xaml
    /// </summary>
    public partial class MassPaymentWindow : Window
    {
        private double totalMoney;
        public static ObservableCollection<PaymentDetail> paymentDetails { get; set; } = new ObservableCollection<PaymentDetail>();
        public MassPaymentWindow()
        {
            InitializeComponent();
        }

        public void LoadPaymentDetailFromDB()
        {
            List<PaymentDetail> paymentDetail = new List<PaymentDetail>();
            paymentDetail.Clear();
            List<string> rooms = new List<string>();
            rooms.Clear();
            rooms.AddRange(RentInfo.GetRoomNameByCustomerID(((Customer)cbCustomerName.SelectedItem).IdCardNumber));
            foreach (var item in rooms)
                paymentDetail.AddRange(PaymentDetail.GetPaymentDetailByRoomName(item));

            paymentDetails.Clear();
            foreach (var item in paymentDetail)
                paymentDetails.Add(item);
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
            List<Room> rooms = new List<Room>();
            rooms.AddRange(Room.GetRoomsByCustomerID(((Customer)cbCustomerName.SelectedItem).IdCardNumber));

            int flag = 0;

            foreach (var item in rooms)
                if (RentInfo.UpdateChechoutDate(item.Name) && RoomRentalDetail.DeleteRoomRentalDetail(item.Name) 
                    && Room.UpdateRoomStatus(item.Name) && RevenueReport.InsertRevenueReport(item.Name, item.Type, RentInfo.GetDateCheckin(item.Name), DateTime.Now, totalMoney)
                    && PaymentDetail.DeletePaymentDetailByRoomName(item.Name))
                    flag += 1;                
            
            if(flag == rooms.Count)
                MessageBox.Show("Thanh toán thành công!");            
            else
                MessageBox.Show("Thanh toán không thành công! Vui lòng thực hiện lại thao tác!");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbCustomerName.ItemsSource = Customer.GetCustomers();
            cbCustomerName.DisplayMemberPath = "Name";
            //cbCustomerName.SelectedValuePath = "Address";
            cbCustomerName.SelectedIndex = 0;
        }
        
        private string TotalFee => string.Format("{0:N0}",PaymentDetail.CalculateTotalMoney(((Customer)cbCustomerName.SelectedItem).IdCardNumber));

        private void CbCustomerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbAddress.Text = ((Customer)cbCustomerName.SelectedItem).Address;
            txbCustomerID.Text = ((Customer)cbCustomerName.SelectedItem).IdCardNumber.ToString();
            //Update payment detail
            List<Room> rooms = new List<Room>();
            rooms.Clear();
            rooms.AddRange(Room.GetRoomsByCustomerID(((Customer)cbCustomerName.SelectedItem).IdCardNumber));

            DateTime dateRent = new DateTime();
            DateTime datePay = new DateTime();

            foreach (var item in rooms)
            {
                dateRent = RentInfo.GetDateCheckin(item.Name);
                datePay = DateTime.Now;
                TimeSpan daysRent = datePay.Subtract(dateRent);

                double roomPrice = (float)item.Price * (daysRent.Days + 1); // Tiền phòng gốc theo ngày thuê (chưa tính phụ thu).
                int quantum = 0;
                if (RoomRentalDetail.GetQuantumCustomerInRoom(item.Name) > RoomType.GetMaxCustomerInRoom(item.Type))
                    quantum = RoomRentalDetail.GetQuantumCustomerInRoom(item.Name) - RoomType.GetMaxCustomerInRoom(item.Type);  // Số lượng khách vượt quá số khách tối đa theo quy định của loại phòng đó.
                double surchargeCustomerType = RoomRentalDetail.GetSurchargeCustomerInRoom(item.Name) / 100; // Phụ thu theo loại khách trong phòng.
                double surchargeQuantumCustomer = CustomerSurcharge.GetSurchargeByQuantum(quantum) / 100; // Phụ thu theo số khách vượt quá số khách tối đa theo quy định của loại phòng đó.
                totalMoney = roomPrice + roomPrice * surchargeCustomerType + roomPrice * surchargeQuantumCustomer;

                PaymentDetail.UpdatePaymentDetail(item.Name, daysRent.Days + 1, totalMoney);
            }

            //Calculate total fee
            txbTotalFee.Text = TotalFee;

            LoadPaymentDetailFromDB();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            rooms.Clear();
            rooms.AddRange(Room.GetAll());

            HotelManager.gui.RoomListUC.RoomList.Clear();
            foreach (var item in rooms)
                HotelManager.gui.RoomListUC.RoomList.Add(item);
            CollectionViewSource.GetDefaultView(HotelManager.gui.RoomListUC.RoomList).Refresh();
        }
    }
}
