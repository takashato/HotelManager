using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManager.db
{
    class DatabaseManager
    {
        //private const string CONN_STR = "server=localhost;user=root;database=hotelmanager;port=3306;password=;charset=utf8";
        private const string CONN_STR = "server=103.27.238.234;user=takashat_hm;database=takashat_hm;port=3306;password=hotelmanager123;charset=utf8";

        public static DatabaseManager Instance { get; set; } = new DatabaseManager(CONN_STR);

        public static MySqlConnection Conn {
            get
            {
                return Instance.Connection;
            }
        }


        public MySqlConnection Connection { get; set; }

        public DatabaseManager(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        public void Initialize()
        {
            Console.WriteLine("Connecting to Database...");
            try
            {
                Connection.Open();
                Console.WriteLine("Connected to Database.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Không thể kết nối đến database.\nVui lòng kiểm tra lại thông tin cấu hình.\n\nInfo: " + ex.Message, "Lỗi");
                Application.Current.Shutdown();
            }
        }
    }
}
