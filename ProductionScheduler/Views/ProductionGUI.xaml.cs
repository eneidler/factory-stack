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
using ProductionScheduler;
using ProductionScheduler.Models;

namespace ProductionScheduler.Views
{
    /// <summary>
    /// Interaction logic for ProductionGUI.xaml
    /// </summary>
    public partial class ProductionGUI : Window
    {
        public ProductionGUI()
        {
            InitializeComponent();
        }

        private void MenuExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AboutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            AboutFactoryStackInfoView infoView = new AboutFactoryStackInfoView();
            infoView.ShowDialog();
        }
    }
}
