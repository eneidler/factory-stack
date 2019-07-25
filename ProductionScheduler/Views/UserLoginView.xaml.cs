using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProductionScheduler.Views
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : UserControl
    {
        public UserLoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var query = from c in context.Users
                            where c.Username == UsernameTextbox.Text && c.Password == PasswordTextbox.Password
                            select c;

                // This will raise an exception if entity not found
                // Use SingleOrDefault instead
                var username = query.SingleOrDefault();

                if (query != null)
                {
                    if(username != null)
                    {
                        ViewStateManager.Instance.ChangeCurrentView(ViewOptions.PressView);
                        ViewStateManager.Instance.SetCurrentUser(UsernameTextbox.Text);
                        ViewStateManager.Instance.SetUserAccessLevel(UsernameTextbox.Text);
                    }
                    if (username == null)
                        MessageBox.Show("Invalid Username or Password. Please try again.", "Invalid Credentials");
                }
                  
            }
        }
    }
}
