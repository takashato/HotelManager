using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.db.model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }
        public EStatus Status { get; set; } = EStatus.Available;

        public static List<Room> GetAll()
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.Query<Room>("SELECT * FROM room INNER JOIN room_type ON room.type=room_type.type").ToList();
            }
        }
        public enum EStatus
        {
            NotAvailable = 0,
            Available = 1,
        }
    }
}
