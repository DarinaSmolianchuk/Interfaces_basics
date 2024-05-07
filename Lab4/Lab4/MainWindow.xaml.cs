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
            list.SelectedIndex = 0;
            list.Focus();
            list.DataContext = myTable.TableLoad();
        }
    }
}