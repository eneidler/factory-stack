using ProductionScheduler.Models;
using ProductionScheduler.Services;
using System;
using System.Linq;
using System.Windows;

namespace ProductionScheduler.Views {
    /// <summary>
    /// Interaction logic for ProductionGUI.xaml
    /// </summary>
    public partial class ProductionGUI : Window {
        public ProductionGUI() {
            InitializeFirstTimePressData();
            InitializeComponent();
        }

        private void MenuExitButton_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

        private void AboutMenuButton_Click(object sender, RoutedEventArgs e) {
            AboutFactoryStackInfoView infoView = new AboutFactoryStackInfoView();
            infoView.ShowDialog();
        }

        private void InitializeFirstTimePressData() {
            using (ProductionSchedulerContext context = new ProductionSchedulerContext()) {
                var largePressQuery = context.Presses.Where(p => p.PressNumber == "Large Press");
                var initLargePress = largePressQuery.FirstOrDefault();
                if (initLargePress == null) {
                    context.Presses.Add(new Press() { PressNumber = "Large Press" });
                }

                var smallPressQuery = context.Presses.Where(p => p.PressNumber == "Small Press");
                var initSmallPress = smallPressQuery.FirstOrDefault();
                if (initSmallPress == null) {
                    context.Presses.Add(new Press() { PressNumber = "Small Press" });
                }

                var leftVacuumPressQuery = context.Presses.Where(p => p.PressNumber == "Left Vacuum Press");
                var initLeftVacuumPress = leftVacuumPressQuery.FirstOrDefault();
                if (initLeftVacuumPress == null) {                   
                    context.Presses.Add(new Press() { PressNumber = "Left Vacuum Press" });
                }

                var rightVacuumPressQuery = context.Presses.Where(p => p.PressNumber == "Right Vacuum Press");
                var initRightVacuumPress = rightVacuumPressQuery.FirstOrDefault();
                if (initRightVacuumPress == null) {
                    context.Presses.Add(new Press() { PressNumber = "Right Vacuum Press" });
                }

                context.SaveChanges();
            }
        }
    }
}
