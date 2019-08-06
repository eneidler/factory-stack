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


        private ICommand _addPartWindowCommand;
        private ICommand _addPressWindowCommand;
        private ICommand _addMoldWindowCommand;
        private ICommand _addJobWindowCommand;

        public ProductionGUIViewModel()
        {
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

        public ICommand AddJobWindowCommand
        {
            get => _addJobWindowCommand = new RelayCommand<object>(_ => NewAddJobWindow());
        }

        private void NewAddJobWindow()
        {
            AddJobView addJobView = new AddJobView();
            addJobView.ShowDialog();
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
