using ProductionScheduler.Interfaces;
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
    /// Interaction logic for AddPressView.xaml
    /// </summary>
    public partial class AddPressView : Window, ITextValidation
    {
        public AddPressView()
        {
            InitializeComponent();
        }

        private void AddPressButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ProductionSchedulerContext())
            {
                var query = from p in context.Parts
                            where p.PartNumber == PressNameTextblock.Text
                            select p;

                var pressNumber = query.SingleOrDefault();

                if (AllTextboxesHaveEntries() != true)
                    MessageBox.Show("All fields are required. Please enter data into textboxes.", "Invalid Entry Attempt", MessageBoxButton.OK, MessageBoxImage.Error);

                if (AllTextboxesHaveEntries() == true)
                {
                    if (pressNumber == null)
                    {
                        var press = new Press()
                        {
                            PressNumber = PressNameTextblock.Text,
                            PressCapacity = PressCapacityTextblock.Text,
                        };
                        context.Presses.Add(press);
                        context.SaveChanges();

                        MessageBox.Show("Record added successfully!", "Record Added", MessageBoxButton.OK, MessageBoxImage.Information);
                        SetTextBoxesNull();
                    }
                    if (pressNumber != null)
                    {
                        MessageBox.Show("This resource already exists.", "Resource entry error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CancelPressAddButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave this screen?", "Cancel New Press", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.OK:
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }

        public void SetTextBoxesNull()
        {
            PressNameTextbox.Text = null;
            PressCapacityTextbox.Text = null;
        }

        public bool AllTextboxesHaveEntries()
        {
            if (
            PressNameTextbox.Text == "" ||
            PressCapacityTextbox.Text == ""
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
