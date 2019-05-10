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
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public int StaffID { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }

        public static List<RentInfo> GetAllCurrentByRoomID(int roomID)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RentInfo>("SELECT * FROM rent_info WHERE room_id = @roomID AND checkin_date >= CURDATE() AND checkout_date IS NULL", new { roomID = roomID } ).ToList();
            }
        }
    }
}
