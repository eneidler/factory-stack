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
using System.Windows.Shapes;

namespace ProductionScheduler.Views
{
    /// <summary>
    /// Interaction logic for AddPartView.xaml
    /// </summary>
    public partial class AddPartView : Window
    {
        public AddPartView()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave this screen?", "Cancel New Part", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    this.Close();
                    break;          
                case MessageBoxResult.Cancel:

                    break;
            }
        }
    }
}
