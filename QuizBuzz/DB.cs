using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuzz
{
    class DB
    {
        readonly MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            
            
            //("server=91.208.99.2;"port=1223;user=colindic_testdb;password=password;database=UserLogin;");
            //("server=91.208.99.2:1223;database=colindic_testdb;uid=colindic_testdb;password=password;");
            //("server=91.208.99.2.1223;Initial Catalog=colindic_testdb;User Id=colindic_testdb;Password=password;");
            // ("server=localhost;port=3306;user=root;password=caxclnc7DuSgJMIh;database=userlogins");

        public void OpenConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();                
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
