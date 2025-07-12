using System.Windows;
using ToDoApp.WpfClient.ViewModels;

namespace ToDoApp.WpfClient
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;
        }
    }
}