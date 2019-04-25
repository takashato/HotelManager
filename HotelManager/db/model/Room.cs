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
        public decimal Price { get; set; }
        public string Note { get; set; }
        public EStatus Status { get; set; } = EStatus.Available;

        public string PriceStr => string.Format("{0:N0}", Price);
        public string StatusStr
        {
            get
            {
                if (Status == EStatus.Available)
                    return "Còn trống";
                if (Status == EStatus.NotAvailable)
                    return "Đã thuê";
                return "Chưa set??";
            }
        }

        public enum EStatus
        {
            NotAvailable = 0,
            Available = 1,
        }

        public static List<Room> GetAll()
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.Query<Room>("SELECT * FROM room INNER JOIN room_type ON room.type=room_type.type").ToList();
            }
        }

        public static bool InsertRoom(string name, string type, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var room = new Room { Name = name, Type = type, Note = note };

                if (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room WHERE name = @Name", new { Name = name }) > 0)
                    return false;

                try
                {
                    return conn.Execute("INSERT INTO room(Name, Type, Note) VALUES(@Name, @Type, @Note)", room) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool DeleteRoom(string name)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Execute("DELETE FROM room WHERE name = @name", new { name = name }) > 0;                       
            }
        }

        public static void UpdateRoom(string newName, string oldName, string newType, string newNote)
        {
            using (var conn = DatabaseManager.Conn)
            {               
                conn.Execute("UPDATE room SET name = '" + newName + "', type = '" + newType + "', note = '" + newNote + "' WHERE name = '" + oldName + "'");
            }
        }

        public static string GetRoomNoteByName(string name)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.QueryFirstOrDefault<string>("SELECT note FROM room WHERE name = @name", new { name = name });
            }
        }
        
    }
}
