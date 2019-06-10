using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public string PriceStr => string.Format("{0:N0}", Price);
        public int MaxCustomer { get; set; }

        public static List<RoomType> GetRoomType()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RoomType>("SELECT type AS Type, price AS Price, max_customer AS MaxCustomer, note AS Note FROM `room_type`").ToList();
            }
        }

        public static bool InsertRoomType(string type, decimal price, string note, int maxCustomer)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var roomType = new RoomType { Type = type, Price = price, MaxCustomer = maxCustomer, Note = note };

                if (!IsAvailable(type, price))
                    return false;

                try
                {
                    return conn.Execute("INSERT INTO room_type(type, price, max_customer, note) VALUES(@Type, @Price, @MaxCustomer, @Note)", roomType) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static bool UpdateRoomType(string newRoomType, decimal newPrice, int newMaxCustomer, string newNote, string oldRoomType)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    if (IsAvailable(newRoomType, newPrice) || newRoomType.Equals(oldRoomType))
                    {
                        return conn.Execute("UPDATE room_type SET type = @Type, price = @Price, max_customer = @MaxCustomer, note = @Note WHERE type = @OldType", new { Type = newRoomType, Price = newPrice, MaxCustomer = newMaxCustomer, Note = newNote, OldType = oldRoomType }) > 0;             
                    }
                    else
                        return false;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static bool DeleteRoomType(string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Execute("DELETE FROM room_type WHERE type = @type", new { type = type }) > 0;
            }
        }

        public static bool IsAvailable(string type, decimal price)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room_type WHERE type = @Type", new { Type = type }) + conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room_type WHERE price = @Price", new { Price = price})) <= 0;
            }
        }

        public static int GetMaxCustomerInRoom(string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT max_customer FROM room_type WHERE type = @Type", new { Type = type});
            }
        }
    }
}
