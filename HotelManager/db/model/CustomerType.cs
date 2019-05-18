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
        public double Surcharge { get; set; }
        public string Note { get; set; }


        public static List<CustomerType> GetCustomerType()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<CustomerType>("SELECT * FROM customer_type").ToList();
            }
        }

        public static bool DeleteCustomerType(string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    return conn.Execute("DELETE FROM customer_type WHERE type = @type", new { type = type }) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static bool InsertCustomerType(string type, double surcharge, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var customerType = new CustomerType { Type = type, Surcharge = surcharge, Note = note };
                List<CustomerType> customerTypes = new List<CustomerType>();

                customerTypes = conn.Query<CustomerType>("SELECT * FROM `customer_type`").ToList();

                foreach (var item in customerTypes)
                    if (item.Type.CompareTo(type) == 0 || item.Surcharge == surcharge)
                        return false;
                try
                {
                    return conn.Execute("INSERT INTO customer_type(Type, Surcharge, Note) VALUES(@Type, @Surcharge, @Note)", customerType) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
