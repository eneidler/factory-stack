using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.Views;

namespace ProductionScheduler.ViewModels
{
    public sealed class ProductionGUIViewModel : BaseViewModel
    {

        private IList<string> _selectedPresses;
        private ICommand _addPartWindowCommand;
        private ICommand _addPressWindowCommand;
        private ICommand _addMoldWindowCommand;

        public ProductionGUIViewModel()
        {
        }

        public IList<string> SelectedPresses
        {
            get
            {
                return _selectedPresses;
            }
            set
            {
                _selectedPresses = value;
                NotifyOnPropertyChanged(nameof(SelectedPresses));
            }
        }

        public ICommand AddPartWindowCommand
        {
            get => _addPartWindowCommand = new RelayCommand<object>(_ => NewAddPartWindow());
        }

        public ICommand AddPressWindowCommand
        {
            get => _addPressWindowCommand = new RelayCommand<object>(_ => NewAddPressWindow());
        }

        public ICommand AddMoldWindowCommand
        {
            get => _addMoldWindowCommand = new RelayCommand<object>(_ => NewAddMoldWindow());
        }


        private void NewAddPartWindow()
        {
            AddPartView addPartView = new AddPartView();
            addPartView.ShowDialog();
        }

        private void NewAddPressWindow()
        {
            AddPressView addPressView = new AddPressView();
            addPressView.ShowDialog();
        }

        private void NewAddMoldWindow()
        {
            AddMoldView addMoldView = new AddMoldView();
            addMoldView.ShowDialog();
        }
    }
}
