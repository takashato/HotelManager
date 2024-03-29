﻿using System;
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
        public long IdCardNumber { get; set; }
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

        public static bool InsertCustomer(string name, string address, long idCardNumber, string type)
        {
            using (var conn = DatabaseManager.Conn)
            {
                var customer = new Customer { Name = name, Address = address, IdCardNumber = idCardNumber, Type = type };
               
                List<long> listIDNumber = new List<long>();
                conn.Query<long>("SELECT id_card_number FROM customer").ToList();

                foreach (var item in listIDNumber)
                    if (item == idCardNumber)
                        return false;
                try
                {
                    return conn.Execute("INSERT INTO customer(name, address, id_card_number, type) VALUES(@Name, @Address, @IdCardNumber, @Type)", customer) > 0;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public static List<Customer>GetCustomers()
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.Query<Customer>("SELECT name AS Name, id_card_number AS IdCardNumber, customer.address AS Address, type AS Type FROM customer INNER JOIN room_rental_detail ON customer.id_card_number = room_rental_detail.customer_id").ToList();
            }
        }

        public static string GetCustomerNameByID(long ID)
        {
            using (var conn = DatabaseManager.Conn)
            {
                return conn.QueryFirstOrDefault<string>("SELECT name FROM customer WHERE id_card_number = @IdCardNumber", new { IdCardNumber = ID }).ToString();
            }
        }
    }
}
