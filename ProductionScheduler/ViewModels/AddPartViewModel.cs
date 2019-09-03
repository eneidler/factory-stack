using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels {
    internal class AddPartViewModel : BaseViewModel {
        private Mold _selectedDatabaseMold;
        private Mold _selectedActiveMold;
        private ObservableCollection<Mold> _activeListMolds = new ObservableCollection<Mold>();
        private ICommand _addSelectedDatabaseMoldToActiveListCommand;
        private ICommand _removeSelectedActiveMoldFromActiveListCommand;
        private ICommand _addNewMoldToDatabaseCommand;

        public AddPartViewModel() {

        }

        public Mold SelectedDatabaseMold {
            get => _selectedDatabaseMold;
            set {
                _selectedDatabaseMold = value;
                NotifyOnPropertyChanged(nameof(SelectedDatabaseMold));
            }
        }

        public Mold SelectedActiveMold {
            get => _selectedActiveMold;
            set {
                _selectedActiveMold = value;
                NotifyOnPropertyChanged(nameof(SelectedActiveMold));
            }
        }

        public ObservableCollection<Mold> ActiveListMolds {
            get => _activeListMolds;
            set {
                _activeListMolds = value;
                NotifyOnPropertyChanged(nameof(SelectedDatabaseMold));
                NotifyOnPropertyChanged(nameof(ActiveListMolds));
            }
        }

        public ICommand AddNewMoldToDatabaseCommand => _addNewMoldToDatabaseCommand = new RelayCommand<object>(_ => AddNewMoldToDatabase());

        public ICommand AddSelectedDatabaseMoldToActiveListCommand => _addSelectedDatabaseMoldToActiveListCommand = new RelayCommand<object>(_ => AddSelectedDatabaseMoldToActiveList());

        public ICommand RemoveSelectedActiveMoldFromActiveListCommand => _removeSelectedActiveMoldFromActiveListCommand = new RelayCommand<object>(_ => RemoveSelectedActiveMoldFromActiveList());

        private void AddNewMoldToDatabase() {
            AddMoldView addMoldView = new AddMoldView();
            addMoldView.ShowDialog();
        }


        private void AddSelectedDatabaseMoldToActiveList() {
            if (!_activeListMolds.Contains(_selectedDatabaseMold))
                _activeListMolds.Add(_selectedDatabaseMold);
            else
                MessageBox.Show("This mold is already in your active list!", "Error Adding Mold", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RemoveSelectedActiveMoldFromActiveList() {
            _activeListMolds.Remove(_selectedActiveMold);
        }
    }
}
