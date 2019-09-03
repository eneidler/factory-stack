using ProductionScheduler.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.Views {
    /// <summary>
    /// Interaction logic for AddJobView.xaml
    /// </summary>
    public partial class AddJobView : Window, IFieldValidation {

        //ProductionSchedulerContext _context = new ProductionSchedulerContext();

        public AddJobView() {
            InitializeComponent();
        }


        //UNDONE: The following code was used for accessing database and
        //populating the Part Number combo box. It can be removed once final testing
        //is complete for the Job Add feature.

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    CollectionViewSource partViewSource = ((CollectionViewSource)(this.FindResource("partViewSource")));
        //    _context.Parts.Load();
        //    partViewSource.Source = _context.Parts.Local;
        //}

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave this screen?", "Cancel New Job", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            switch (result) {
                case MessageBoxResult.OK:
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }

        public void ClearAllFields() {
            //TODO: Finish or remove ITextValidation implementation
            //private void btnClick(object sender, RoutedEventArgs e)
            //{
            //    var btn = sender as Button;
            //    btn.Command.Execute(btn.CommandParameter);
            //}
        }

        public bool AllFieldsHaveEntries() {
            return true; //TODO: Add actual logic. This is in place for program testing.
        }

        private void JobQuantityTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            foreach (char c in e.Text) {
                if (!char.IsDigit(c)) {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
