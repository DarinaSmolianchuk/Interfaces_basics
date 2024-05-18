using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement uielement in GridBlock.Children)
                if (uielement is Button)
                    ((Button)uielement).Click += buttonClick;

            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            CommandBindings.Add(saveCommand);
            CommandBinding replaceCommand = new CommandBinding(ApplicationCommands.Delete, execute_Delete, canExecute_Delete);
            CommandBindings.Add(replaceCommand);
        }

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            string data = (string)((Button)e.OriginalSource).Content;
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
                    double result = Math.Round(Double.Parse(new DataTable().Compute(calcDataBlock.Text, null).ToString()), 6);
                    historyBlock.Text = historyBlock.Text + calcDataBlock.Text + "=" + result.ToString().Replace(',', '.') + "\n";
                    calcDataBlock.Text = result.ToString().Replace(',', '.');
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

        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
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
        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Assembly.GetEntryAssembly().Location;
            save.Filter = "Text files (*.txt)|*.txt";
            if (save.ShowDialog() == true)
            {
                string data = historyBlock.Text.Trim();
                byte[] info = new UTF8Encoding(true).GetBytes(data);
                FileStream stream = (FileStream)save.OpenFile();
                stream.Write(info, 0, info.Length);
            }
        }

        void canExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
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
        void execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            historyBlock.Text = "";
        }
    }
}
