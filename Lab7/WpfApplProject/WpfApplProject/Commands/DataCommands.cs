using System.Windows.Input;

namespace WpfApplProject.Commands
{
    class DataCommands
    {
        public static RoutedCommand Undo { get; set; }
        public static RoutedCommand New { get; set; }
        public static RoutedCommand Replace { get; set; }
        public static RoutedCommand Save { get; set; }
        public static RoutedCommand Find { get; set; }
        public static RoutedCommand Delete { get; set; }

        static DataCommands()
        {
            //Колекція об'єктів
            InputGestureCollection inputs = new InputGestureCollection();

            //Команди
            inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+Z"));
            Undo = new RoutedCommand("Undo", typeof(DataCommands), inputs);

            inputs.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));
            New = new RoutedCommand("New", typeof(DataCommands), inputs);

            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            Replace = new RoutedCommand("Replace", typeof(DataCommands), inputs);

            inputs.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            Save = new RoutedCommand("Save", typeof(DataCommands), inputs);

            inputs.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+F"));
            Find = new RoutedCommand("Find", typeof(DataCommands), inputs);

            inputs.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+D"));
            Delete = new RoutedCommand("Delete", typeof(DataCommands), inputs);
        }

    }
}
