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
        public static RoutedCommand BackspaceCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement uielement in GridBlock.Children)
                if (uielement is Button)
                    ((Button)uielement).Click += buttonClick;

            btnBackspace.Command = BackspaceCommand;
            CommandBinding backspaceCommandBinding = new CommandBinding(BackspaceCommand, ExecuteBackspace, CanExecuteBackspace);
            CommandBindings.Add(backspaceCommandBinding);

            
            CommandBinding replaceCommand = new CommandBinding(ApplicationCommands.Delete, execute_Delete, canExecute_Delete);
            CommandBindings.Add(replaceCommand);
        }

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            //string data = null;
            //if (((Button)e.OriginalSource).Content is string)
            //{
            //    data = (string)((Button)e.OriginalSource).Content;
            //}
            //else if (((Button)e.OriginalSource).Content is Image)
            //{
            //    if (((Button)e.OriginalSource).Name == "btnBackspace")
            //    {
            //        if (!string.IsNullOrEmpty(calcDataBlock.Text))
            //        {
            //            calcDataBlock.Text = calcDataBlock.Text.Substring(0, calcDataBlock.Text.Length - 1);
            //        }
            //        return;
            //    }
            //}
            string data = null;
            if (((Button)e.OriginalSource).Content is string)
            {
                data = (string)((Button)e.OriginalSource).Content;
            }
            else if (((Button)e.OriginalSource).Content is Image)
            {
                // Handle the case where the content is an Image.
                // For example, you could check the Name of the button:
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

        private void CanExecuteBackspace(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(calcDataBlock.Text);
        }

        private void ExecuteBackspace(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(calcDataBlock.Text))
            {
                calcDataBlock.Text = calcDataBlock.Text.Substring(0, calcDataBlock.Text.Length - 1);
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
