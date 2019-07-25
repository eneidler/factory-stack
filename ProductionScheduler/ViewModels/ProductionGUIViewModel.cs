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

        public ProductionGUIViewModel()
        {
        }

        public ICommand AddPartWindowCommand
        {
            get => _addPartWindowCommand = new RelayCommand<object>(_ => NewAddPartWindow());
        }


        private void NewAddPartWindow()
        {
            AddPartView addPartView = new AddPartView();
            addPartView.Show();
        }
    }
}
