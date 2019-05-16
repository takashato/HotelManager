using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class CustomerSurcharge
    {
        public int Quantum { get; set; }
        public float Surcharge { get; set; }
        public string Note { get; set; }

        public static List<CustomerSurcharge> GetAll()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<CustomerSurcharge>("SELECT * FROM customer_surcharge").ToList();
            }
        }
    }
}
