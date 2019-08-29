using ProductionScheduler.Interfaces;
using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for AddMoldView.xaml
    /// </summary>
    public partial class AddMoldView : Window, IFieldValidation
    {
        ProductionSchedulerContext _context = new ProductionSchedulerContext();

        public AddMoldView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource pressViewSource = ((CollectionViewSource)(this.FindResource("pressViewSource")));
            // Load is an extension method on IQueryable,
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query,
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method
            // creates entity objects and adds them to the context.
            _context.Presses.Load();

            // After the data is loaded call the DbSet<T>.Local property
            // to use the DbSet<T> as a binding source.
            pressViewSource.Source = _context.Presses.Local;

            
        }

        private void CancelNewMoldButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave this screen?", "Cancel New Mold", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.OK:
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }

        private void AddNewMoldButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var query = from m in context.Molds
                            where m.MoldNumber == MoldNumberTextbox.Text
                            select m;

                var moldNumber = query.SingleOrDefault();

                if (AllFieldsHaveEntries() != true)
                    MessageBox.Show("All fields are required. Please enter data into textboxes.", "Invalid Entry Attempt", MessageBoxButton.OK, MessageBoxImage.Error);

                if (AllFieldsHaveEntries() == true)
                {
                    if (moldNumber == null)
                    {
                        IList<Press> pressesToAdd = new List<Press>();

                        foreach(string press in ActivePressNumberListbox.Items)
                        {
                            var pressQuery = from p in context.Presses
                                           where p.PressNumber == press
                                           select p;

                            var newPress = pressQuery.SingleOrDefault();

                            pressesToAdd.Add(newPress);
                        }

                        var mold = new Mold()
                        {
                            MoldNumber = MoldNumberTextbox.Text,
                            NumberOfCavities = int.Parse(NumberOfCavitiesTextbox.Text),
                            Presses = pressesToAdd,
                            Parts = new List<Part>()
                        };
                        context.Molds.Add(mold);
                        context.SaveChanges();

                        MessageBox.Show("Record added successfully!", "Record Added", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearAllFields();
                    }
                    if (moldNumber != null)
                    {
                        MessageBox.Show("This mold number already exists.", "Mold entry error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }    
        

        public void ClearAllFields()
        {
            MoldNumberTextbox.Text = "";
            NumberOfCavitiesTextbox.Text = "";
            var viewModel = (AddMoldViewModel)DataContext;
            viewModel.ActiveListPresses.Clear();
            
        }

        public bool AllFieldsHaveEntries()
        {
            if (
            MoldNumberTextbox.Text == "" ||
            NumberOfCavitiesTextbox.Text == "" ||
            ActivePressNumberListbox.Items.Count <= 0
            )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
