using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lab4
{
    public class AdoAssistant
    {
        // Отримуємо рядок з'єднання з файлу App.config
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
                
        // Метод читання даних з DataTable
        DataTable dt = null; // Посилання на об'єкт DataTable
        public DataTable TableLoad()
        {
            if (dt != null) return dt; // Завантажимо таблицю лише один раз
            // Заповнюємо об'єкт таблиці даними з БД
            dt = new DataTable();
            // Створюємо об'єкт підключення
            using (SqlConnection сonnection = new SqlConnection(connectionString))
            {
                SqlCommand command = сonnection.CreateCommand(); // Створюємо об'єкт команди
                SqlDataAdapter adapter = new SqlDataAdapter(command); // Створюємо об'єкт читання
                //Завантажує дані 
                command.CommandText = "Select Id," + "FullName," +
                "Unit, Position, PhoneNumber From Employees";
                try
                {
                    // Метод сам відкриває БД і сам її закриває
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка підключення до БД:\n" + e);
                 
                }
            }
            return dt;
        }
    }
}