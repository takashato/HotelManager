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
                if (!IsAvailable(quantum, surcharge, -1))
                    return false;
                try
                {
                    return conn.Execute("INSERT INTO customer_surcharge(Quantum, Surcharge, Note) VALUES(@Quantum, @Surcharge, @Note)", customerSurcharge) > 0;
                }
                catch (Exception)
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
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool UpdateCustomerSurcharge(int newQuantum, double newSurcharge, string newNote, int oldQuantum)
        {
            using (var conn = DatabaseManager.Conn)
            {
                try
                {
                    if (IsAvailable(newQuantum, newSurcharge, oldQuantum))
                    {
                        conn.Execute("UPDATE customer_surcharge SET quantum = '" + newQuantum + "', surcharge = '" + newSurcharge + "', note = '" + newNote + "' WHERE quantum = '" + oldQuantum + "'");
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool IsAvailable(int quantum, double surcharge, int oldQuantum)
        {
            using (var conn = DatabaseManager.Conn)
            {
                if (quantum == oldQuantum)
                {
                    return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM customer_surcharge WHERE surcharge = @Surcharge AND quantum <> @Quantum", new { Surcharge = surcharge, Quantum = quantum }) <= 0;
                }
                else
                    return (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM customer_surcharge WHERE quantum = @Quantum", new { Quantum = quantum }) + 
                        conn.ExecuteScalar<int>("SELECT COUNT(*) FROM customer_surcharge WHERE surcharge = @Surcharge", new { Surcharge = surcharge })) <= 0;
            }
        }

        public static double GetSurchargeByQuantum(int quantum)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.ExecuteScalar<double>("SELECT surcharge FROM customer_surcharge WHERE quantum = @Quantum", new { Quantum = quantum});
            }
        }
    }
}
