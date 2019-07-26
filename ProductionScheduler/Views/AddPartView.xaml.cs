using ProductionScheduler.Models;
using ProductionScheduler.Services;
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

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var query = from p in context.Parts
                            where p.PartNumber == PartNumberTextbox.Text
                            select p;

                var partNumber = query.SingleOrDefault();


                
                    if (partNumber == null)
                    {
                        if (AreAllTextBoxesNullOrEmpty() == false)
                        {
                            var part = new Part()
                            {
                                PartNumber = PartNumberTextbox.Text,
                                ProductFamily = ProductFamilyTextbox.Text,
                                ProductDescription = ProductDescriptionTextbox.Text,
                                CureTimeInMinutes = int.Parse(CureTimeTextbox.Text),
                                Molds = new List<Mold>
                                    {
                                        new Mold{MoldNumber = PartNumberMoldsTextbox.Text}
                                    }
                            };
                            context.Parts.Add(part);
                            context.SaveChanges();

                            MessageBox.Show("Record added successfully!", "Record Added", MessageBoxButton.OK, MessageBoxImage.Information);
                            SetTextBoxesNull();
                        }
                        if (AreAllTextBoxesNullOrEmpty() == true)
                            MessageBox.Show("A valid part number is required at a minimum. Please enter data into textboxes.", "Invalid Entry Attempt", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    if (partNumber != null)
                    {
                        MessageBox.Show("This part number already exists.", "Part entry error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }             
            }          
        }

        public void SetTextBoxesNull()
        {
            PartNumberTextbox.Text = null;
            ProductFamilyTextbox.Text = null;
            ProductDescriptionTextbox.Text = null;
            CureTimeTextbox.Text = null;
            PartNumberMoldsTextbox.Text = null;
        }

        public bool AreAllTextBoxesNullOrEmpty()
        {
            if(
            PartNumberTextbox.Text == null &&
            ProductFamilyTextbox.Text == null &&
            ProductDescriptionTextbox.Text == null &&
            CureTimeTextbox.Text == null &&
            PartNumberMoldsTextbox.Text == null
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }                             
    }
}
