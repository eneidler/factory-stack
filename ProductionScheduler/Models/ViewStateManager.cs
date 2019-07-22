using ProductionScheduler.Interfaces;
using ProductionScheduler.Models.UserLogin;
using ProductionScheduler.Services;
using ProductionScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.Models
{
    public sealed class ViewStateManager : BaseViewModel
    {
        private static readonly object padlock = new object();
        private static ViewStateManager instance = null;
        private int _switchView;
        private string _currentUser;
        private AccessLevels _accessLevel;
        private ICommand _logoutCommand;

        private ViewStateManager()
        {

        }

        public static ViewStateManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ViewStateManager();
                    }
                    return instance;
                }
            }
        }

        public int SwitchView
        {
            get
            {
                return _switchView;
            }
            set
            {
                _switchView = value;
                NotifyOnPropertyChanged(nameof(SwitchView));
            }
        }




        public string CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }



        public AccessLevels AccessLevel
        {
            get
            {
                return _accessLevel;
            }
            set { _accessLevel = value; }
        }

        public ICommand LogoutCommand
        {
            get
            {
                _logoutCommand = new RelayCommand<object>(_ => LogoutCurrentUser());
                return _logoutCommand;
            }

        }


        public void ChangeCurrentView(ViewOptions newView)
        {
            SwitchView = (int)newView;
        }

        public void SetCurrentUser(string username)
        {
            CurrentUser = username;
        }

        public void SetUserAccessLevel(string currentUser)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var accessLevel = (from c in context.Users
                            where c.Username == currentUser
                            select c).SingleOrDefault();

                AccessLevel = accessLevel.AccessLevel;
            }
        }

        public void LogoutCurrentUser()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "FactoryStack.IO Logout", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    SwitchView = (int)ViewOptions.LoginView;
                    break;
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
                    
        }
    }
}
