﻿using System;
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

        public static List<RoomType> GetRoomType()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RoomType>("SELECT * FROM `room_type`").ToList();
            }
        }

        public static int InsertRoomType(string type, decimal price, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var roomType = new RoomType { Type = type, Price = price, Note = note };
                List<RoomType> roomTypes = new List<RoomType>();

                roomTypes = conn.Query<RoomType>("SELECT * FROM `room_type`").ToList();

                foreach (var item in roomTypes)
                    if (item.Type.CompareTo(type) == 0 || item.Price == price)
                        return 0;
                conn.Execute("INSERT INTO room_type(Type, Price, Note) VALUES(@Type, @Price, @Note)", roomType);
                return 1;
            }
        }

        public static void DeleteRoomType(string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                conn.Execute("DELETE FROM room_type WHERE type = @type", new { type = type });
            }
        }

    }
}
