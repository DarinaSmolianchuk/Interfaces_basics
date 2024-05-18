using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        public static RoutedCommand BackspaceCommand = new RoutedCommand();

        Lab6_DBEntities context;
        List<CalculatorHistory> db_calc_history;

        private string currentExpression = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            context = new Lab6_DBEntities();
            db_calc_history = new List<CalculatorHistory>();

            foreach (UIElement uielement in GridBlock.Children)
                if (uielement is Button)
                    ((Button)uielement).Click += ButtonClick;

            btnBackspace.Command = BackspaceCommand;
            CommandBinding BackspaceCommandBinding = new CommandBinding(BackspaceCommand, Execute_Backspace, CanExecute_Backspace);
            CommandBindings.Add(BackspaceCommandBinding);

            CommandBinding ReplaceCommand = new CommandBinding(ApplicationCommands.Delete, Execute_Delete, CanExecute_Delete);
            CommandBindings.Add(ReplaceCommand);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var emp in context.CalculatorHistory)
                db_calc_history.Add(emp);
            DataBaseDir.ItemsSource = db_calc_history;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            string data = null;
            if (((Button)e.OriginalSource).Content is string)
            {
                data = (string)((Button)e.OriginalSource).Content;
            }
            else if (((Button)e.OriginalSource).Content is Image)
            {
                if (((Button)e.OriginalSource).Name == "btnBackspace")
                {
                    return;
                }
            }

            if (data == "C")
            {
                calcDataBlock.Text = "";
            }
            else if (data == "=")
            {
                if (calcDataBlock.Text != "" && !calcDataBlock.Text.EndsWith("/")
                    && !calcDataBlock.Text.EndsWith("*") && !calcDataBlock.Text.EndsWith("+")
                    && !calcDataBlock.Text.EndsWith("-") && !calcDataBlock.Text.EndsWith("."))
                {
                    // Зберігаємо поточний вираз
                    currentExpression = calcDataBlock.Text;
                    // Обчислюємо результат
                    int result;
                    try
                    {
                        result = Convert.ToInt32(new DataTable().Compute(calcDataBlock.Text, null));
                    }
                    catch
                    {
                        calcDataBlock.Text = "Error";
                        return;
                    }
                    // Оновлюємо історію та текстове поле результату
                    historyBlock.Text = historyBlock.Text + calcDataBlock.Text + "=" + result.ToString() + "\n";
                    calcDataBlock.Text = result.ToString();
                    // Зберігаємо в базу даних
                    SaveCalculationToDatabase(currentExpression, result);
                }
            }
            else if (data == "-" || data == "+" || data == "*" || data == "/" || data == ".")
            {
                if (!calcDataBlock.Text.EndsWith("/") && !calcDataBlock.Text.EndsWith("*") && !calcDataBlock.Text.EndsWith("+") && !calcDataBlock.Text.EndsWith("-") && calcDataBlock.Text != "")
                    calcDataBlock.Text += data;
            }
            else if (data == ".")
            {
                if (!calcDataBlock.Text.EndsWith("."))
                    calcDataBlock.Text += data;
            }
            else if (data == "Очистити" || data == "Завантажити")
            {

            }
            else
            {
                calcDataBlock.Text += data;
            }
        }

        private void CanExecute_Backspace(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(calcDataBlock.Text);
        }
        private void Execute_Backspace(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(calcDataBlock.Text))
            {
                calcDataBlock.Text = calcDataBlock.Text.Substring(0, calcDataBlock.Text.Length - 1);
            }
        }

        void CanExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            if (historyBlock.Text.Trim().Length == 0)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            };
        }
        void Execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            historyBlock.Text = "";
        }

        private void SaveCalculationToDatabase(string expression, int result)
        {
            DateTime currentTime = DateTime.Now;
            CalculatorHistory newRecord = new CalculatorHistory
            {
                MathExpression = expression,
                Result = result,
                DateTime = currentTime
            };
            // Додати запис до контексту та зберегти зміни
            context.CalculatorHistory.Add(newRecord);
            context.SaveChanges();
            // Оновити локальну колекцію та відображення в DataGrid
            db_calc_history.Add(newRecord);
            DataBaseDir.ItemsSource = null;
            DataBaseDir.ItemsSource = db_calc_history;
        }

        private void FindByMathExpression(object sender, RoutedEventArgs e)
        {
            string searchTerm = MathExpressionBlock.Text;

            var foundRecords = from record in context.CalculatorHistory
                               where record.MathExpression.Contains(searchTerm)
                               select record;

            db_calc_history.Clear();
            foreach (var record in foundRecords)
            {
                db_calc_history.Add(record);
            }

            DataBaseDir.ItemsSource = null;
            DataBaseDir.ItemsSource = db_calc_history;
        }

        private void FindByResult(object sender, RoutedEventArgs e)
        {


            int searchResult = 0;
            var foundRecords = from record in context.CalculatorHistory
                               where record.Result == searchResult
                               select record;

            db_calc_history.Clear();
            foreach (var record in foundRecords)
            {
                db_calc_history.Add(record);
            }

            DataBaseDir.ItemsSource = null;
            DataBaseDir.ItemsSource = db_calc_history;
        }


    }
}
