using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class StaffType
    {
        public int Level { get; set; }
        public string Type { get; set; }

        public static List<string> GetStaffTypes()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<string>("SELECT `type` FROM `staff_type`").ToList();
            }
        }

    }
}
