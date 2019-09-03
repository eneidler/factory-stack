using ProductionScheduler.Interfaces;
using ProductionScheduler.Models.UserLogin;
using ProductionScheduler.Services;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels {
    internal class UserLoginViewModel : BaseViewModel, ISystemLogin {

        private string _loginUsername;
        private string _loginPassword;
        private ICommand _loginCommand;

        public UserLoginViewModel() {

        }

        public string LoginUsername {
            get => _loginUsername;
            set {
                _loginUsername = value;
                NotifyOnPropertyChanged(nameof(LoginUsername));
            }
        }
        public string LoginPassword {
            get => _loginPassword;

            set {
                _loginPassword = value;
                NotifyOnPropertyChanged(nameof(LoginPassword));
            }
        }
        public AccessLevels AccessLevel { get; set; }

        public ICommand LoginCommand => _loginCommand = new RelayCommand<object>(_ => LoginUser());


        public void LoginUser() {
            //TODO: Add using loging for accessing the DB and comparing credentials
            MessageBox.Show("Test message for RelayCommand");


        }
    }
}
