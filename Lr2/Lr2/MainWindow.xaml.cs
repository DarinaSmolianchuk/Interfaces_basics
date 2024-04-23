using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Lr2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Створення прив'язки та приєднання обробників
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            //Реєстрація прив'язки
            CommandBindings.Add(saveCommand);

            //Відкрити
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);
            CommandBindings.Add(openCommand);

            //Стерти
            CommandBinding deleteCommand = new CommandBinding(ApplicationCommands.Delete, execute_Delete, canExecute_Delete);
            CommandBindings.Add(deleteCommand);
        }

        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }
        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            System.IO.File.WriteAllText("D:\\Мои документы\\0КПІ\\3 курс\\6 семестр\\Декларативне прог граф інт\\2\\myFile.txt", inputTextBox.Text);
            MessageBox.Show("The file was saved!");
        }

        //Відкрити
        private void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            string defaultFolderPath = @"D:\Мои документы\0КПІ\3 курс\6 семестр\Декларативне прог граф інт\2";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text files (*.txt)|*.txt";
            open.InitialDirectory = defaultFolderPath;
            if (open.ShowDialog() == true)
                inputTextBox.Text = File.ReadAllText(open.FileName);
        }

        //Стерти
        private void canExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }
        private void execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Text = "";
        }
    }
}
