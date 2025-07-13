using System.Windows;
using ToDoApp.WpfClient.ViewModels;

namespace ToDoApp.WpfClient
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}