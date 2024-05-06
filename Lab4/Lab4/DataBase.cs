using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace Lab4
{
    public class DataBase
    {
        public static DataTable loadDataTable()
        {
            //Виймаємо у полі рядок з'єднання з файлу App.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //Запит
            string strSQL = "select * from Employees;";

            //Заповнення DataTable
            DataTable data = new DataTable();

            // Підготовка об'єкта команди
            SqlCommand command = new SqlCommand(strSQL, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            // Заповнення DataTable даними з об'єкта читання
            data.Load(dataReader);
            dataReader.Close();
            connection.Close();
            
            return data;
        }

    }

}
