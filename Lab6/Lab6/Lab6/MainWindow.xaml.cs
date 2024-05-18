using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        public static RoutedCommand BackspaceCommand = new RoutedCommand();
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement uielement in GridBlock.Children)
                if (uielement is Button)
                    ((Button)uielement).Click += ButtonClick;

            btnBackspace.Command = BackspaceCommand;
            CommandBinding BackspaceCommandBinding = new CommandBinding(BackspaceCommand, Execute_Backspace, CanExecute_Backspace);
            CommandBindings.Add(BackspaceCommandBinding);

            CommandBinding ReplaceCommand = new CommandBinding(ApplicationCommands.Delete, Execute_Delete, CanExecute_Delete);
            CommandBindings.Add(ReplaceCommand);
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
    }
}