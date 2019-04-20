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
        public static List<RoomType> GetRoomType()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<RoomType>("SELECT * FROM `room_type`").ToList();
            }
        }

    }
}
