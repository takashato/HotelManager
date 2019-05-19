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
        public ELevel Level { get; set; }

        public enum ELevel
        {
            Receptionist = 1,
            Manager = 2,
            Administrator = 3
        }
            

        public static Staff GetByUsername(string username)
        {
            using (var conn = DatabaseManager.Conn) // Syntatic connection usage
            {
                return conn.QueryFirstOrDefault<Staff>("SELECT * FROM `staff` WHERE `username` = @username", new { username = username });
            }
        }

        public static List<Staff> GetAll()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<Staff>("SELECT * FROM staff INNER JOIN staff_type on staff.level = staff_type.level ").ToList();
            }
        }

        public static bool InsertStaff(string username, string password, string fullname, string level)
        {
            using (var conn = DatabaseManager.Conn)
            {
                string encryptPassword = BCrypt.Net.BCrypt.HashPassword(password);
                int lv = 1;
                switch (level)
                {
                    case "Manager":
                        lv = 2;
                        break;
                    case "Receptionist":
                        lv = 1;
                        break;
                    case "Administrator":
                        lv = 3;
                        break;
                }

                if (!IsAvailableUsername(username))
                    return false;

                try
                {
                    return conn.Execute("INSERT INTO staff(username, password, fullname, level, createdDate) VALUES(@username, @password, @fullname, @level, @createdDate)",
                        new { username = username, password = encryptPassword, fullname = fullname, level = lv, createdDate = DateTime.Now }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool UpdateStaff(string newPassword, string newFullname, string newLevel, string username)
        {
            int lv = 1;
            switch (newLevel)
            {
                case "Manager":
                    lv = 2;
                    break;
                case "Receptionist":
                    lv = 1;
                    break;
                case "Administrator":
                    lv = 3;
                    break;
            }
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    string password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    conn.Execute("UPDATE staff SET fullname = '" + newFullname + "', level = '" + lv + "', password = '" + password + "' WHERE username = '" + username + "'");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool DeleteStaff(string username)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Execute("DELETE FROM staff WHERE username = @username", new { username = username }) > 0;
            }
        }

        public static bool IsAvailableUsername(string username)
        {
            if ("".Equals(username)) return false;

            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM staff WHERE username = @username", new { username = username }) <= 0;
            }
        }
        
    }
}
