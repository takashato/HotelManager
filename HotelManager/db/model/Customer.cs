using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HotelManager.db.model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal IdCardNumber { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        //public string TypeStr
        //{
        //    get
        //    {
        //        if (Type == EType.Inland)
        //            return "Nội địa";
        //        if (Type == EType.Foreign)
        //            return "Nước ngoài";
        //        return "Chưa set??";
        //    }
        //}
        //public enum EType
        //{
        //    Inland = 0,
        //    Foreign = 1
        //}

        public static int InsertCustomer(string name, string address, decimal idCardNumber, string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var customer = new Customer { Name = name, Address = address, IdCardNumber = idCardNumber, Type = type };
               
                List<decimal> listIDNumber = new List<decimal>();
                conn.Query<decimal>("SELECT id_card_number FROM customer").ToList();

                foreach (var item in listIDNumber)
                    if (item == idCardNumber)
                        return 0;
                conn.Execute("INSERT INTO customer(Name, ) VALUES()", customer);
                return 1;
            }
        }
    }
}
