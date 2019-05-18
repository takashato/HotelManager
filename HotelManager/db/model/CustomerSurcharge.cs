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
        public double Surcharge { get; set; }
        public string Note { get; set; }

        public static List<CustomerSurcharge> GetAll()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<CustomerSurcharge>("SELECT * FROM customer_surcharge").ToList();
            }
        }

        public static bool InsertCustomerSurcharge(int quantum, double surcharge, string note)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var customerSurcharge = new CustomerSurcharge { Quantum = quantum, Surcharge = surcharge, Note = note };
                List<CustomerSurcharge> customerSurcharges = new List<CustomerSurcharge>();

                customerSurcharges = conn.Query<CustomerSurcharge>("SELECT * FROM `customer_surcharge`").ToList();

                foreach (var item in customerSurcharges)
                    if (item.Quantum == quantum || item.Surcharge == surcharge)
                        return false;
                try
                {
                    return conn.Execute("INSERT INTO customer_surcharge(Quantum, Surcharge, Note) VALUES(@Quantum, @Surcharge, @Note)", customerSurcharge) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool DeleteCustomerSurcharge(int quantum)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    return conn.Execute("DELETE FROM customer_surcharge WHERE quantum = @quantum", new { quantum = quantum }) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
