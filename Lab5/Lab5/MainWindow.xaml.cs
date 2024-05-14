using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lab5
{
    
    public partial class MainWindow : Window
    {
        EmployeesAndUnitsEntities context;
        List<Employees> employees;
        List<Units> units;


        public MainWindow()
        {
            InitializeComponent();
            context = new EmployeesAndUnitsEntities();
            employees = new List<Employees>();
            units = new List<Units>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var emp in context.Employees)
                employees.Add(emp);
            EmployeesDir.ItemsSource = employees;

            foreach (var emp in context.Units)
                units.Add(emp);
            UnitsDir.ItemsSource = units;

            var query = from emp in context.Employees
                        join unit in context.Units on emp.Id_Unit equals unit.Id_Unit
                        select new
                        {
                            ID = emp.Id,
                            EmployeeName = emp.FullName,
                            UnitName = unit.Unit,
                            Position = emp.Position,
                            PhoneNumber = emp.PhoneNumber
                        };

            EmloyeesAndUnitsDir.ItemsSource = query.ToList();
        }

        private void FindByUnitName(object sender, RoutedEventArgs e)
        {
            var query = from emp in context.Employees
                        join unit in context.Units on emp.Id_Unit equals unit.Id_Unit
                        where unit.Unit == UnitBlock.Text
                        select new
                        {
                            ID = emp.Id,
                            EmployeeName = emp.FullName,
                            UnitName = unit.Unit,
                            Position = emp.Position,
                            PhoneNumber = emp.PhoneNumber
                        };

            gridByUnitName.ItemsSource = query.ToList();

            int employeeCount = query.Count();
            UnitCountBlock.Text = employeeCount.ToString();
        }

        private void FindByPosition(object sender, RoutedEventArgs e)
        {
            var query = from emp in context.Employees
                        join unit in context.Units on emp.Id_Unit equals unit.Id_Unit
                        where emp.Position == PositionBlock.Text
                        select new
                        {
                            ID = emp.Id,
                            EmployeeName = emp.FullName,
                            UnitName = unit.Unit,
                            Position = emp.Position,
                            PhoneNumber = emp.PhoneNumber
                        };

            gridByPosition.ItemsSource = query.ToList();

            int employeeCount = query.Count();
            PositionCountBlock.Text = employeeCount.ToString();
        }

    }
}
