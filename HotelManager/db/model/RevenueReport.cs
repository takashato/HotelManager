using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class RevenueReport
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public double Amount { get; set; }

        public static List<RevenueReport> GetAllByDate(DateTime startDay, DateTime endDay)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RevenueReport>("SELECT * FROM revenue_report WHERE checkout_date BETWEEN DATE(@StartDay) AND DATE(@EndDay)", new { StartDay = startDay, EndDay = endDay}).ToList();
            }
        }

        public static bool InsertRevenueReport(string roomName, string roomType, DateTime checkinDate, DateTime checkoutDate, double amount)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var revenueReport = new RevenueReport { RoomName = roomName, RoomType = roomType, CheckinDate = checkinDate, CheckoutDate = checkoutDate, Amount = amount };
                return conn.Execute("INSERT INTO revenue_report(room_name, room_type, checkin_date, checkout_date, amount) VALUES(@RoomName, @RoomType, @CheckinDate, @CheckoutDate, @Amount)", revenueReport) > 0;
            }
        }
    }
}
