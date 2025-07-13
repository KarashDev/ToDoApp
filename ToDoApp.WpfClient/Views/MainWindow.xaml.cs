using System.Windows;
using ToDoApp.WpfClient.Services;
using ToDoApp.WpfClient.ViewModels;

namespace ToDoApp.WpfClient
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            var apiClient = new TodoApiClient("https://localhost:7040/");
            _vm = new MainViewModel(apiClient);

            DataContext = _vm;
        }
    }
}