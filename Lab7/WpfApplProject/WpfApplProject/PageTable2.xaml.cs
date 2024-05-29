using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace WpfApplProject
{
    public partial class PageTable2 : Page
    {
        Lab6_DBEntities context;
        List<CalculatorHistory> db_calc_history;
        private string currentExpression = string.Empty;
        public PageTable2()
        {
            InitializeComponent();
            context = new Lab6_DBEntities();
            db_calc_history = new List<CalculatorHistory>();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var emp in context.CalculatorHistory)
                db_calc_history.Add(emp);
            DataBaseDir.ItemsSource = db_calc_history;
        }
        public void SaveCalculationToDatabase(string expression, int result)
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
            int searchResult;
            if (!int.TryParse(ResultBlock.Text, out searchResult))
            {
                return;
            }

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
        private void UndoSearching(object sender, RoutedEventArgs e)
        {
            db_calc_history.Clear();
            foreach (var record in context.CalculatorHistory)
            {
                db_calc_history.Add(record);
            }
            DataBaseDir.ItemsSource = null;
            DataBaseDir.ItemsSource = db_calc_history;
        }
    }
}
