using System;
using System.Windows;

namespace ProductionScheduler.Views {
    /// <summary>
    /// Interaction logic for ProductionGUI.xaml
    /// </summary>
    public partial class ProductionGUI : Window {
        public ProductionGUI() {
            InitializeComponent();
        }

        private void MenuExitButton_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

        private void AboutMenuButton_Click(object sender, RoutedEventArgs e) {
            AboutFactoryStackInfoView infoView = new AboutFactoryStackInfoView();
            infoView.ShowDialog();
        }
    }
}
