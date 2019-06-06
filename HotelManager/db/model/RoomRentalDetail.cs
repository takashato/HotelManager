using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class RoomRentalDetail
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string CustomerName { get; set; }
        public long CustomerID { get; set; }
        public string Address { get; set; }
        public string CustomerType { get; set; }

        public static bool InsertRoomRentalDetail(string roomName, string customerName, long customerID, string address, string customerType)
        {
            using (var conn = DatabaseManager.Conn)
            {
                RoomRentalDetail roomRentalDetail = new RoomRentalDetail { RoomName = roomName, CustomerName = customerName, CustomerID = customerID, Address = address, CustomerType = customerType};
                try
                {
                    return conn.Execute("INSERT INTO room_rental_detail(room_name, customer_name, customer_id, address, customer_type) VALUE(@RoomName, @CustomerName, @CustomerID, @Address, @CustomerType)", roomRentalDetail) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static bool DeleteRoomRentalDetail(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Execute("DELETE FROM room_rental_detail WHERE room_name = @RoomName", new { RoomName = roomName}) > 0;
            }
        }

        public static List<RoomRentalDetail>GetRentalDetailByRoomName(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RoomRentalDetail>("SELECT address AS Address, customer_id AS CustomerID, room_name AS RoomName, customer_name AS CustomerName, customer_type AS CustomerType FROM room_rental_detail WHERE room_name = @RoomName", new { RoomName = roomName}).ToList();              
            }
        }

        public static int GetQuantumCustomerInRoom(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room_rental_detail WHERE room_name = @RoomName", new { RoomName = roomName});
            }
        }

        public static int GetQuantumForeignCustomerInRoom(string roomName)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room_rental_detail WHERE room_name = @RoomName AND customer_type = @CustomerType", new { RoomName = roomName, CustomerType = "Nước ngoài"});
            }
        }
    }
}
