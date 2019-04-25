using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Surcharge { get; set; }
        public string Note { get; set; }


        public static List<CustomerType> GetCustomerType()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<CustomerType>("SELECT * FROM customer_type").ToList();
            }
        }
    }
}
