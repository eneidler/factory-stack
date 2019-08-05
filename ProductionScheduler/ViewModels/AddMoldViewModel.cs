using ProductionScheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels
{
    class AddMoldViewModel : BaseViewModel
    {
        private ObservableCollection<string> _activeListPresses = new ObservableCollection<string>();
        private string _selectedDatabasePress;
        private ICommand _addSelectedDatabasePressToActiveListCommand;
        private string _selectedActivePress;
        private RelayCommand<object> _removeSelectedActivePressCommand;

        public AddMoldViewModel()
        {

        }

        public string SelectedDatabasePress
        {
            get => _selectedDatabasePress;
            set
            {
                _selectedDatabasePress = value;
                NotifyOnPropertyChanged(nameof(SelectedDatabasePress));
            }
        }

        public string SelectedActivePress
        {
            get => _selectedActivePress;
            set
            {
                _selectedActivePress = value;
                NotifyOnPropertyChanged(nameof(SelectedActivePress));
            }
        }

        public ObservableCollection<string> ActiveListPresses
        {
            get => _activeListPresses;
            set
            {
                _activeListPresses = value;
                NotifyOnPropertyChanged(nameof(SelectedDatabasePress));
                NotifyOnPropertyChanged(nameof(ActiveListPresses));
            }
        }

        public ICommand AddSelectedDatabasePressToActiveListCommand
        {
            get => _addSelectedDatabasePressToActiveListCommand = new RelayCommand<object>(_ => AddSelectedDatabasePressToActiveList());
        }

        public ICommand RemoveSelectedActivePressCommand
        {
            get => _removeSelectedActivePressCommand = new RelayCommand<object>(_ => RemoveSelectedActivePressFromActiveList());
        }

        private void AddSelectedDatabasePressToActiveList()
        {
            if (!_activeListPresses.Contains(_selectedDatabasePress))
                _activeListPresses.Add(_selectedDatabasePress);
            else
                MessageBox.Show("This press is already in your active list!", "Error Adding Press", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RemoveSelectedActivePressFromActiveList()
        {
            _activeListPresses.Remove(_selectedActivePress);
        }

    }
}
