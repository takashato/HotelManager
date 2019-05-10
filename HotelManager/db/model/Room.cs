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
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public EStatus Status { get; set; } = EStatus.Available;

        public string PriceStr => string.Format("{0:N0}", Price);

        public List<RentInfo> _RentInfos { get; set; } = null;

        public string StatusStr => GetStatus();

        public enum EStatus
        {
            NotAvailable = 0,
            Available = 1,
        }

        public void RetrieveRentInfo()
        {
            _RentInfos = RentInfo.GetAllCurrentByRoomID(ID);
        }

        public string GetStatus()
        {
            if (IsAvailable()) return "Còn trống";
            else return "Đã thuê";
        }

        public bool IsAvailable()
        {
            return _RentInfos == null || _RentInfos.Count <= 0;
        }

        public static List<Room> GetAll()
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.Query<Room>("SELECT * FROM room INNER JOIN room_type ON room.type=room_type.type").ToList();
            }
        }

        public static Task<IEnumerable<Room>> GetAllAsync()
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.QueryAsync<Room>("SELECT * FROM room INNER JOIN room_type ON room.type=room_type.type");
            }
        }

        public static string GetRoomNoteByName(string name)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.QueryFirstOrDefault<string>("SELECT note FROM room WHERE name = @name", new { name = name });
            }
        }

        public static bool InsertRoom(string name, string type, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var room = new Room { Name = name, Type = type, Note = note };

                if (!IsAvailableName(name))
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

        public static bool UpdateRoom(string newName, string oldName, string newType, string newNote)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    if (!newName.Equals(oldName) && IsAvailableName(newName) || newName.Equals(oldName))
                    {
                        conn.Execute("UPDATE room SET name = '" + newName + "', type = '" + newType + "', note = '" + newNote + "' WHERE name = '" + oldName + "'");
                        return true;
                    } 
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        
        public static bool IsAvailableName(string name)
        {
            if ("".Equals(name)) return false;

            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM room WHERE name = @Name", new { Name = name }) <= 0;
            }
        }
    }
}
