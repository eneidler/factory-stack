using ProductionScheduler.Interfaces;
using ProductionScheduler.Models;
using ProductionScheduler.Services;
using ProductionScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddPartView.xaml
    /// </summary>
    public partial class AddPartView : Window, ITextValidation
    {

        ProductionSchedulerContext _context = new ProductionSchedulerContext();

        public AddPartView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource moldViewSource = ((CollectionViewSource)(this.FindResource("moldViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            _context.Molds.Load();
            moldViewSource.Source = _context.Molds.Local;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave this screen?", "Cancel New Part", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.OK:
                    this.Close();
                    break;          
                case MessageBoxResult.Cancel:

                    break;
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var query = from p in context.Parts
                            where p.PartNumber == PartNumberTextbox.Text
                            select p;

                var partNumber = query.SingleOrDefault();

                if (AllFieldsHaveEntries() != true)
                    MessageBox.Show("All fields are required. Please enter data into textboxes.", "Invalid Entry Attempt", MessageBoxButton.OK, MessageBoxImage.Error);

                if (AllFieldsHaveEntries() == true)
                {
                    if (partNumber == null)
                    {

                        var viewModel = (AddPartViewModel)DataContext;
                        IList<Mold> moldsToAdd = viewModel.ActiveListMolds;

                        var part = new Part()
                        {
                            PartNumber = PartNumberTextbox.Text,
                            ProductFamilyCategory = ProductFamilyTextbox.Text,
                            ProductDescription = ProductDescriptionTextbox.Text,
                            CureTimeInMinutes = int.Parse(CureTimeTextbox.Text),
                            Molds = new List<Mold>()                          
                        };         
                        
                        context.Parts.Add(part); //New part is added to the DB with an empty list so following foreach can add existing molds to created list

                        foreach (Mold mold in moldsToAdd)
                        {
                            Mold newMold = context.Molds.FirstOrDefault(m => m.MoldNumber == mold.MoldNumber);
                            part.Molds.Add(newMold);
                        }

                        context.SaveChanges();

                        MessageBox.Show("Record added successfully!", "Record Added", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearAllFields();

                    }                  
                    if (partNumber != null)
                    {
                        MessageBox.Show("This part number already exists.", "Part entry error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }              
            }          
        }

        public void ClearAllFields()
        {
            PartNumberTextbox.Text = null;
            ProductFamilyTextbox.Text = null;
            ProductDescriptionTextbox.Text = null;
            CureTimeTextbox.Text = null;
            var viewModel = (AddPartViewModel)DataContext;
            viewModel.ActiveListMolds.Clear();
        }

        public bool AllFieldsHaveEntries()
        {
            if(
            PartNumberTextbox.Text == "" ||
            ProductFamilyTextbox.Text == "" ||
            ProductDescriptionTextbox.Text == "" ||
            CureTimeTextbox.Text == "" ||
            ActiveMoldNumberListbox.Items.Count <= 0
            )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //HACK: Constrains text input for part number to alpha-numeric characters.
        private void PartNumberTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void RefreshMoldListButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource moldViewSource = ((CollectionViewSource)(this.FindResource("moldViewSource")));
            _context.Molds.Load();
            moldViewSource.Source = _context.Molds.Local;
        }
    }
}
