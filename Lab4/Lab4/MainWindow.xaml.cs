using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AdoAssistant myTable = new AdoAssistant();

            // Встановлюємо вибір першого елемента у списку
            list.SelectedIndex = 0;

            // Встановлюємо фокус на списку
            list.Focus();

            // Встановлюємо джерело даних списку, яке отримуємо з методу TableLoad() класу AdoAssistant
            list.DataContext = myTable.TableLoad();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (!IsAllLetters(FullNameBlock.Text) || FullNameBlock.Text == "")
            {
                MessageBox.Show("Ім'я має бути буквеним і не пустим!");
                return;
            }
            if (!IsAllLetters(PositionBlock.Text) || PositionBlock.Text == "")
            {
                MessageBox.Show("Посада має бути буквеною і не пустою!");
                return;
            }
            if (!IsAllDigits(PhoneNumberBlock.Text) || PhoneNumberBlock.Text == "")
            {
                MessageBox.Show("Телефон має бути числовим і не пустим!");
                return;
            }
            if (UnitBlock.Text == "")
            {
                MessageBox.Show("Підрозділ має бути не пустим!");
                return;
            }
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
            using (SqlConnection сonnection = new SqlConnection(connectionString))
            {
                сonnection.Open();
                string commandText = "UPDATE Employees SET FullName = @FullName, Unit = @Unit, Position = @Position, PhoneNumber = @PhoneNumber WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(commandText, сonnection);
                cmd.Parameters.AddWithValue("@FullName", FullNameBlock.Text);
                cmd.Parameters.AddWithValue("@Unit", UnitBlock.Text);
                cmd.Parameters.AddWithValue("@Position", PositionBlock.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumberBlock.Text);
                cmd.Parameters.AddWithValue("@Id", IdBlock.Text);
                try
                {
                    cmd.ExecuteNonQuery();
                    status = 0;
                    MessageBox.Show("Дані робітника оновлено!");
                    AdoAssistant myTable = new AdoAssistant();
                    list.SelectedIndex = 0;
                    list.DataContext = myTable.TableLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка додавання до БД: " + ex);
                }
                сonnection.Close();
            }

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;                                                                                                       // Створюємо об'єкт підключення
            using (SqlConnection сonnection = new SqlConnection(connectionString))
            {
                сonnection.Open();
                string commandText = "DELETE FROM Employees WHERE ID = " + IdBlock.Text + ";";
                SqlCommand cmd = new SqlCommand(commandText, сonnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    status = 0;
                    AdoAssistant myTable = new AdoAssistant();
                    list.SelectedIndex = 0;
                    list.DataContext = myTable.TableLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка додавання до БД: " + ex);
                }
                сonnection.Close();
            }
        }

        private byte status = 0;
        private void NewClick(object sender, RoutedEventArgs e)
        {
            if (status == 0)
            {
                list.SelectedIndex = -1;
                list.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                MessageBox.Show("Можна вводити дані для нового об'єкту");
                status = 1;
            }
            else if (status == 1)
            {
                if (!IsAllLetters(FullNameBlock.Text) || FullNameBlock.Text == "")
                {
                    MessageBox.Show("Ім'я має бути буквеним і не пустим!");
                    return;
                }
                if (!IsAllLetters(PositionBlock.Text) || PositionBlock.Text == "")
                {
                    MessageBox.Show("Посада має бути буквеною і не пустою!");
                    return;
                }
                if (!IsAllDigits(PhoneNumberBlock.Text) || PhoneNumberBlock.Text == "")
                {
                    MessageBox.Show("Телефон має бути числовим і не пустим!");
                    return;
                }
                if (UnitBlock.Text == "")
                {
                    MessageBox.Show("Підрозділ має бути не пустим!");
                    return;
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
                using (SqlConnection сonnection = new SqlConnection(connectionString))
                {
                    сonnection.Open();
                    string commandText = "INSERT INTO Employees (FullName, Unit, Position, PhoneNumber) VALUES (@FullName, @Unit, @Position, @PhoneNumber)";
                    SqlCommand cmd = new SqlCommand(commandText, сonnection);
                    cmd.Parameters.AddWithValue("@FullName", FullNameBlock.Text);
                    cmd.Parameters.AddWithValue("@Unit", UnitBlock.Text);
                    cmd.Parameters.AddWithValue("@Position", PositionBlock.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumberBlock.Text);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        status = 0;
                        MessageBox.Show("Робітника додано!");
                        AdoAssistant myTable = new AdoAssistant();
                        list.SelectedIndex = 0;
                        list.DataContext = myTable.TableLoad();
                        list.IsEnabled = true;
                        UpdateButton.IsEnabled = true;
                        DeleteButton.IsEnabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка додавання до БД: " + ex);
                    }
                    сonnection.Close();
                }
            }
        }
        private bool IsAllLetters(string input)
        {
            return input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
        private bool IsAllDigits(string input)
        {
            return input.All(c => char.IsDigit(c) || char.IsWhiteSpace(c));
        }
    }
}