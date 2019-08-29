using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProductionScheduler.Interfaces;
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
        private ICommand _scheduleViewCommand;
        //private IList<Job> _bindingJobList;

        public ProductionGUIViewModel()
        {
            //_bindingJobList = new ObservableCollection<Job>(JobManager.Instance.ActiveJobList as IList<Job>);
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

        public ICommand ScheduleViewCommand
        {
            get => _scheduleViewCommand = new RelayCommand<object>(_ => ToggleScheduleView()); //Switches visible user control in main GUI via a data trigger
        }

        public IList<Job> BindingJobList
        {
            get => new ObservableCollection<Job>(JobManager.Instance.ActiveJobList as IList<Job>);
        }

        public Job ActiveJobOne { get => BindingJobList[0]; }

        public Job ActiveJobTwo { get => BindingJobList[1]; }

        public Job ActiveJobThree { get => BindingJobList[2]; }

        public Job ActiveJobFour { get => BindingJobList[3]; }

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

        private void ToggleScheduleView()
        {
            ViewStateManager.Instance.SwitchView = (int)ViewOptions.SchedulingView;
        }
    }
}
