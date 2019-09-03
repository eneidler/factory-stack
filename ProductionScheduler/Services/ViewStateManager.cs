using ProductionScheduler.Models;
using ProductionScheduler.Models.UserLogin;
using ProductionScheduler.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.Services {
    public sealed class ViewStateManager : BaseViewModel {
        private static readonly object _padlock = new object();
        private static ViewStateManager _instance = null;
        private int _switchView;
        private string _currentUser;
        private AccessLevels _accessLevel;
        private ICommand _logoutCommand;
        private bool _adminEnableButtonState;
        private bool _supervisorEnableButtonState;

        private ViewStateManager() {

        }

        public static ViewStateManager Instance {
            get {
                lock (_padlock) {
                    if (_instance == null) {
                        _instance = new ViewStateManager();
                    }
                    return _instance;
                }
            }
        }

        public int SwitchView {
            get => _switchView;
            set {
                _switchView = value;
                NotifyOnPropertyChanged(nameof(SwitchView));
            }
        }




        public string CurrentUser {
            get => _currentUser;
            set => _currentUser = value;
        }



        public AccessLevels AccessLevel {
            get => _accessLevel;
            set => _accessLevel = value;
        }


        public bool AdminEnableButtonState {
            get {
                if (_accessLevel == AccessLevels.Admin)
                    _adminEnableButtonState = true;
                if (_accessLevel != AccessLevels.Admin)
                    _adminEnableButtonState = false;
                return _adminEnableButtonState;
            }
            set => _adminEnableButtonState = value;
        }

        public bool SupervisorEnableButtonState {
            get {
                if (_accessLevel >= AccessLevels.Supervisor)
                    _supervisorEnableButtonState = true;
                if (_accessLevel < AccessLevels.Supervisor)
                    _supervisorEnableButtonState = false;
                return _supervisorEnableButtonState;
            }
            set => _supervisorEnableButtonState = value;
        }




        public ICommand LogoutCommand => _logoutCommand = new RelayCommand<object>(_ => LogoutCurrentUser());




        public void ChangeCurrentView(ViewOptions newView) {
            SwitchView = (int)newView;
        }

        public void SetCurrentUser(string username) {
            CurrentUser = username;
        }

        public void SetUserAccessLevel(string currentUser) {
            using (var context = new ProductionSchedulerContext()) {
                var accessLevel = (from c in context.Users
                                   where c.Username == currentUser
                                   select c).SingleOrDefault();

                AccessLevel = accessLevel.AccessLevel;
            }
        }

        public void LogoutCurrentUser() {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "FactoryStack.IO Logout", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            switch (result) {
                case MessageBoxResult.OK:
                    SwitchView = (int)ViewOptions.LoginView;
                    break;
                case MessageBoxResult.Cancel:
                    //Do nothing
                    break;
            }

        }
    }
}
