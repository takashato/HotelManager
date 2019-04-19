using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.db.model
{
    public class Staff
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Level { get; set; }

        public static Staff GetByUsername(string username)
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.QueryFirstOrDefault<Staff>("SELECT * FROM `staff` WHERE `username` = @username", new { username = username });
            }
        }
    }
}
