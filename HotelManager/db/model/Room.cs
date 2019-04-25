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

        public static int InsertRoom(string name, string type, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var room = new Room { Name = name, Type = type, Note = note };
           
                List<string> roomName = new List<string>();
                roomName = conn.Query<string>("SELECT name FROM room").ToList();
             
                foreach(var item in roomName)
                {
                    if (item.CompareTo(name) == 0)
                        return 0;
                }
                conn.Execute("INSERT INTO room(Name, Type, Note) VALUES(@Name, @Type, @Note)", room);
                
                return 1;
                
            }
            
        }

        public static void DeleteRoom(string name)
        {
            using (var conn = DatabaseManager.Conn)
            {
                conn.Execute("DELETE FROM room WHERE name = @name", new { name = name });                       
            }
        }

        public static void UpdateRoom(string newName, string oldName, string newType, string newNote)
        {
            using (var conn = DatabaseManager.Conn)
            {               
                conn.Execute("UPDATE room SET name = '" + newName + "' WHERE name = '" + oldName + "'");
                conn.Execute("UPDATE room SET type = '" + newType + "' WHERE name = '" + newName + "'");
                conn.Execute("UPDATE room SET note = '" + newNote + "' WHERE name = '" + newName + "'");
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
