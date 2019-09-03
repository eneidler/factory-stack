using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels {
    public sealed class ProductionGUIViewModel : BaseViewModel {


        private ICommand _addPartWindowCommand;
        private ICommand _addPressWindowCommand;
        private ICommand _addMoldWindowCommand;
        private ICommand _addJobWindowCommand;
        private ICommand _scheduleViewCommand;
        private ICommand _toggleJobViewCommand;
        private Job _largePressJobs;
        private Job _smallPressJobs;
        private Job _leftVacuumPressJobs;
        private Job _rightVacuumPressJobs;
        private string _largePressBackgroundColor;

        //private IList<Job> _bindingJobList;

        public ProductionGUIViewModel() {
            //_bindingJobList = new ObservableCollection<Job>(JobManager.Instance.ActiveJobList as IList<Job>);
        }

        public ICommand AddPartWindowCommand => _addPartWindowCommand = new RelayCommand<object>(_ => NewAddPartWindow());

        public ICommand AddPressWindowCommand => _addPressWindowCommand = new RelayCommand<object>(_ => NewAddPressWindow());

        public ICommand AddMoldWindowCommand => _addMoldWindowCommand = new RelayCommand<object>(_ => NewAddMoldWindow());

        public ICommand AddJobWindowCommand => _addJobWindowCommand = new RelayCommand<object>(_ => NewAddJobWindow());

        public ICommand ScheduleViewCommand => _scheduleViewCommand = new RelayCommand<object>(_ => ToggleScheduleView()); //Switches visible user control in main GUI via a data trigger

        public ICommand ToggleJobViewCommand => _toggleJobViewCommand = new RelayCommand<object>(_ => ToggleJobView()); //Switches visible user control in main GUI via a data trigger

        public IList<Job> BindingJobList => new ObservableCollection<Job>(JobManager.Instance.ActiveJobList as IList<Job>);

        public Job LargePressJobs {
            get {
                var query = BindingJobList.Where(j => j.Press.PressNumber == "Large Press");
                var largePressJobs = query.FirstOrDefault();
                return _largePressJobs = largePressJobs;

            }
            set {
                _largePressJobs = value;
                NotifyOnPropertyChanged(nameof(LargePressJobs));
                NotifyOnPropertyChanged(nameof(LargePressBackgroundColor));
            }
        }

        public Job LeftVacuumPressJobs {
            get {
                var query = BindingJobList.Where(j => j.Press.PressNumber == "Left Vacuum Press");
                var leftVacuumPressJobs = query.FirstOrDefault();
                return _leftVacuumPressJobs = leftVacuumPressJobs;

            }
            set {
                _leftVacuumPressJobs = value;
                NotifyOnPropertyChanged(nameof(LeftVacuumPressJobs));
            }
        }

        public Job SmallPressJobs {
            get {
                var query = BindingJobList.Where(j => j.Press.PressNumber == "Small Press");
                var smallPressJobs = query.FirstOrDefault();
                return _smallPressJobs = smallPressJobs;

            }
            set {
                _smallPressJobs = value;
                NotifyOnPropertyChanged(nameof(SmallPressJobs));
            }
        }

        public Job RightVacuumPressJobs {
            get {
                var query = BindingJobList.Where(j => j.Press.PressNumber == "Right Vacuum Press");
                var rightVacuumPressJobs = query.FirstOrDefault();
                return _rightVacuumPressJobs = rightVacuumPressJobs;

            }
            set {
                _rightVacuumPressJobs = value;
                NotifyOnPropertyChanged(nameof(RightVacuumPressJobs));
            }
        }

        //TODO: Determine if there is a better way to control background color for press display buttons
        public string LargePressBackgroundColor {
            get => _largePressBackgroundColor = GetBackgroundColor(LargePressJobs.IsPaused);
            set {
                _largePressBackgroundColor = value;
                NotifyOnPropertyChanged(nameof(LargePressJobs));
            }
        }

        private void NewAddJobWindow() {
            AddJobView addJobView = new AddJobView();
            addJobView.ShowDialog();
        }

        private void NewAddPartWindow() {
            AddPartView addPartView = new AddPartView();
            addPartView.ShowDialog();
        }

        private void NewAddPressWindow() {
            AddPressView addPressView = new AddPressView();
            addPressView.ShowDialog();
        }

        private void NewAddMoldWindow() {
            AddMoldView addMoldView = new AddMoldView();
            addMoldView.ShowDialog();
        }

        private void ToggleScheduleView() {

            switch (ViewStateManager.Instance.SwitchView) {
                case (int)ViewOptions.SchedulingView:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.PressView;
                    break;
                case (int)ViewOptions.PressView:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.SchedulingView;
                    break;
                default:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.PressView;
                    break;
            }
        }

        private void ToggleJobView() {
            switch (ViewStateManager.Instance.SwitchView) {
                case (int)ViewOptions.PressView:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.JobView;
                    break;
                case (int)ViewOptions.JobView:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.PressView;
                    break;
                default:
                    ViewStateManager.Instance.SwitchView = (int)ViewOptions.JobView;
                    break;
            }
        }

        private static string GetBackgroundColor(bool isPaused) {
            string backgroundColor;
            if (isPaused == true)
                backgroundColor = "Yellow";
            else
                backgroundColor = "#CCE0DC";
            return backgroundColor;
        }
    }
}
