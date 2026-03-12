using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using WpfMvvmSkeleton.Core;
using WpfMvvmSkeleton.Models;
using WpfMvvmSkeleton.Services;

namespace WpfMvvmSkeleton.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // ── Private fields ───────────────────────────────────────────────
        private readonly ISampleService _sampleService;

        // ── Constructor — services are injected in, not created here ────
        public MainViewModel(ISampleService sampleService)
        {
            _sampleService = sampleService;

            // Wire commands to their handler methods
            LoadItemsCommand  = new RelayCommand(async _ => await LoadItemsAsync());
            SaveItemCommand   = new RelayCommand(async _ => await SaveItemAsync(),
                                                 _       => SelectedItem != null);
            DeleteItemCommand = new RelayCommand(async _ => await DeleteItemAsync(),
                                                 _       => SelectedItem != null);
            ClearStatusCommand = new RelayCommand(_ => StatusMessage = string.Empty);
        }

        // ════════════════════════════════════════════════════════════════
        //  PROPERTIES  — every field the View binds to lives here.
        //  The setter always calls OnPropertyChanged() so the UI updates.
        // ════════════════════════════════════════════════════════════════

        // ── Item list ────────────────────────────────────────────────────
        private ObservableCollection<SampleItem> _items = new ObservableCollection<SampleItem>();
        public ObservableCollection<SampleItem> Items
        {
            get => _items;
            set { _items = value; OnPropertyChanged(); }
        }

        // ── Selected item in list ─────────────────────────────────────────
        private SampleItem _selectedItem;
        public SampleItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                // Refresh CanExecute on commands that depend on selection
                CommandManager_Refresh();
            }
        }

        // ── Status / feedback bar ─────────────────────────────────────────
        private string _statusMessage = "Ready";
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        // ── Busy indicator (drives ProgressBar / spinner visibility) ──────
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                // Opposite for UI convenience — bind IsEnabled to IsNotBusy
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
        public bool IsNotBusy => !_isBusy;

        // ── Search / filter text ──────────────────────────────────────────
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        // ════════════════════════════════════════════════════════════════
        //  COMMANDS  — bound to Buttons / MenuItems in XAML.
        //  No Click handlers needed in code-behind.
        // ════════════════════════════════════════════════════════════════
        public RelayCommand LoadItemsCommand   { get; }
        public RelayCommand SaveItemCommand    { get; }
        public RelayCommand DeleteItemCommand  { get; }
        public RelayCommand ClearStatusCommand { get; }

        // ════════════════════════════════════════════════════════════════
        //  COMMAND HANDLERS  — private async methods, one per command.
        // ════════════════════════════════════════════════════════════════

        private async Task LoadItemsAsync()
        {
            try
            {
                IsBusy        = true;
                StatusMessage = "Loading…";

                var items = await _sampleService.GetItemsAsync();

                Items.Clear();
                foreach (var item in items)
                    Items.Add(item);

                StatusMessage = $"Loaded {Items.Count} item(s).";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SaveItemAsync()
        {
            if (SelectedItem == null) return;

            try
            {
                IsBusy        = true;
                StatusMessage = "Saving…";

                bool ok = await _sampleService.SaveItemAsync(SelectedItem);

                StatusMessage = ok ? "Saved successfully." : "Save failed.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error saving: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteItemAsync()
        {
            if (SelectedItem == null) return;

            var confirm = MessageBox.Show(
                $"Delete '{SelectedItem.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                IsBusy        = true;
                StatusMessage = "Deleting…";

                bool ok = await _sampleService.DeleteItemAsync(SelectedItem.Id);

                if (ok)
                {
                    Items.Remove(SelectedItem);
                    SelectedItem  = null;
                    StatusMessage = "Deleted.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error deleting: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        // ── Helper: forces WPF to re-evaluate CanExecute on all commands ─
        private void CommandManager_Refresh() =>
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
    }
}
