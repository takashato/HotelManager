using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.db.model
{
    public class RentInfo
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string RoomName { get; set; }
        public string StaffName { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }

        public static List<RentInfo> GetAllCurrentByRoomName(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RentInfo>("SELECT * FROM rent_info WHERE room_name = @roomName AND checkin_date >= CURDATE() AND checkout_date IS NULL", new { RoomName = roomName } ).ToList();
            }
        }

        public static bool InsertCheckinInfo(string roomName, string staffName, string customerName, DateTime checkinDate)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var checkinInfo = new RentInfo { RoomName = roomName, StaffName = staffName, CustomerName = customerName, CheckinDate = checkinDate };
                try
                {
                    return conn.Execute("INSERT INTO rent_info(room_name, staff_name, customer_name, checkin_date) VALUE (@RoomName, @StaffName, @CustomerName, @CheckinDate)", checkinInfo) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static bool FindRoomRented(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM rent_info WHERE room_name = @roomName", new { RoomName = roomName }) > 0;
            }
        }

        public static DateTime GetDateCheckin(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.QueryFirstOrDefault<DateTime>("SELECT checkin_date FROM rent_info WHERE room_name = @RoomName", new { RoomName = roomName}).Date;
            }
        }

        public static bool UpdateChechoutDate(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    return conn.Execute("UPDATE rent_info SET checkout_date = @CheckoutDate WHERE room_name = @RoomName", new { RoomName = roomName, CheckoutDate = DateTime.Now}) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }
    }
}
