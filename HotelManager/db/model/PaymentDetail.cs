using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckinDate { get; set; }
        public int DaysRented { get; set; }
        public int CustomerQuantum { get; set; }
        public int ForeignQuantum { get; set; }
        public double Amount { get; set; }

        public static bool InsertPaymentDetail(string roomName, DateTime checkinDate, int customerQuantum, int foreignQuantum)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var paymentDetail = new PaymentDetail { RoomName = roomName, CheckinDate = checkinDate, CustomerQuantum = customerQuantum, ForeignQuantum = foreignQuantum};
                return conn.Execute("INSERT INTO payment_detail(room_name, checkin_date, customer_quantum, foreign_quantum) VALUES(@RoomName, @CheckinDate, @CustomerQuantum, @ForeignQuantum)", paymentDetail) > 0;
            }
        }

        //public static bool InsertPayment(string roomName, DateTime checkinDate, int customerQuantum, int foreignQuantum, int daysRented, double amount)
        //{
        //    using (var conn = DatabaseManager.Conn)
        //    {
        //        var paymentDetail = new PaymentDetail { RoomName = roomName, CheckinDate = checkinDate, DaysRented = daysRented, CustomerQuantum = customerQuantum, ForeignQuantum = foreignQuantum, Amount = amount };
        //        return conn.Execute("INSERT INTO payment_detail(room_name, checkin_date, days_rented, customer_quantum, foreign_quantum, amount) VALUES(@RoomName, @CheckinDate, @DaysRented, @CustomerQuantum, @ForeignQuantum, @Amount)", paymentDetail) > 0;
        //    }
        //}

        public static bool UpdatePaymentDetail(string roomName, int daysRented, double amount)
        {
            using (var conn = DatabaseManager.Conn)
            {
               try
                {
                    return conn.Execute("UPDATE payment_detail SET days_rented = @DaysRented, amount = @Amount WHERE room_name = @RoomName", new { RoomName = roomName, DaysRented = daysRented, Amount = amount }) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static List<PaymentDetail> GetPaymentDetailByRoomName(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<PaymentDetail>("SELECT room_name AS RoomName, checkin_date AS CheckinDate, days_rented AS DaysRented, customer_quantum AS CustomerQuantum, foreign_quantum AS ForeignQuantum, amount AS Amount FROM payment_detail WHERE room_name = @RoomName", new { RoomName = roomName}).ToList();
            }
        }

        public static double CalculateTotalMoney(long customerId)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<double>("SELECT SUM(amount) FROM payment_detail INNER JOIN rent_info ON payment_detail.room_name = rent_info.room_name WHERE rent_info.customer_id = @CustomerID", new { CustomerID = customerId});
            }
        }
    }
}
