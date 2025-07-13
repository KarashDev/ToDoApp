using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoApp.WpfClient.Models;
using ToDoApp.WpfClient.Services;

namespace ToDoApp.WpfClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITodoApiClient _apiClient;
        private string _newTitle = "";
        private TodoItem? _selectedItem;

        public ObservableCollection<TodoItem> Items { get; } = new();

        public MainViewModel(ITodoApiClient apiClient)
        {
            _apiClient = apiClient;

            AddCommand = new AsyncCommand(AddAsync, () => !string.IsNullOrWhiteSpace(NewTitle));
            DeleteCommand = new AsyncCommand(DeleteAsync, () => SelectedItem != null);

            _ = LoadAsync();
        }

        public string NewTitle
        {
            get => _newTitle;
            set
            {
                if (_newTitle != value)
                {
                    _newTitle = value;
                    OnPropertyChanged();
                    AddCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public TodoItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    DeleteCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public AsyncCommand AddCommand { get; }
        public AsyncCommand DeleteCommand { get; }

        public async Task LoadAsync()
        {
            Items.Clear();
            var items = await _apiClient.GetAllAsync();
            foreach (var item in items)
                Items.Add(item);
        }

        public async Task AddAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTitle))
                return;

            bool success = await _apiClient.CreateAsync(NewTitle);
            if (success)
            {
                NewTitle = "";
                await LoadAsync();
            }
        }

        public async Task DeleteAsync()
        {
            if (SelectedItem == null)
                return;

            bool success = await _apiClient.DeleteAsync(SelectedItem.Id);
            if (success)
            {
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
