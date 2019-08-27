using ProductionScheduler.Interfaces;
using ProductionScheduler.Models;
using ProductionScheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels
{
    class AddJobViewModel : BaseViewModel, ITextValidation
    {

        ProductionSchedulerContext _context = new ProductionSchedulerContext();
        private Press _selectedPressNumber;
        private Mold _selectedMoldNumber;
        private Part _selectedPartNumber;
        private IList<Part> _availablePartList = new ObservableCollection<Part>();
        private IList<Mold> _availableMoldList = new ObservableCollection<Mold>();
        private IList<Press> _availablePressList = new ObservableCollection<Press>();
        private int _jobQuantity;
        private string _additionalNotes;
        private RelayCommand<object> _addJobCommand;

        public AddJobViewModel()
        {

        }

        public Part SelectedPartNumber
        {
            get => _selectedPartNumber;
            set
            {
                _selectedPartNumber = value;
                NotifyOnPropertyChanged(nameof(SelectedPartNumber));
                NotifyOnPropertyChanged(nameof(AvailableMoldList));
            }
        }

        public Mold SelectedMoldNumber
        {
            get => _selectedMoldNumber;
            set
            {
                _selectedMoldNumber = value;
                NotifyOnPropertyChanged(nameof(AvailablePressList));
            }
        }

        public Press SelectedPressNumber
        {
            get => _selectedPressNumber;
            set
            {
                _selectedPressNumber = value;
                NotifyOnPropertyChanged(nameof(AvailablePartList)); //This listener is here so that when the ClearAllFields() method is called, there is a notification to AvailablePartList.
            }
        }


        public IList<Part> AvailablePartList
        {
            get
            {
                    var query = from p in _context.Parts
                                select p;

                    var usableParts = query.ToList();
                    _availablePartList = usableParts;

                return _availablePartList;
            }
        }

        public IList<Mold> AvailableMoldList
        {
            get
            {
                string selectedPartNumber = SelectedPartNumber.PartNumber;
                var moldList = _context.Molds.Where(m => m.Parts.Any(x => x.PartNumber.Contains(selectedPartNumber)));

                List<Mold> usableMolds = moldList.ToList();
 
                _availableMoldList = usableMolds;
                return _availableMoldList;
            }
        }

        public IList<Press> AvailablePressList
        {
            get
            {
                string selectedMoldNumber = SelectedMoldNumber.MoldNumber;
                var pressList = _context.Presses.Where(p => p.Molds.Any(x => x.MoldNumber.Contains(selectedMoldNumber)));

                List<Press> usablePresses = pressList.ToList();

                _availablePressList = usablePresses;
                return _availablePressList;
            }
        }

        public int JobQuantity
        {
            get => _jobQuantity;
            set
            {
                _jobQuantity = value;
                NotifyOnPropertyChanged(nameof(JobQuantity));
            }
        }

        public string AdditionalNotes
        {
            get => _additionalNotes;
            set
            {
                _additionalNotes = value;
                NotifyOnPropertyChanged(nameof(AdditionalNotes));
            }
        }

        public ICommand AddJobCommand
        {
            get => _addJobCommand = new RelayCommand<object>(_ => AddNewJobToDatabase());
        }

        public bool AllFieldsHaveEntries()
        {
            bool fieldsHaveEntries;
            if (SelectedPartNumber != null &&
                SelectedMoldNumber != null &&
                SelectedPressNumber != null &&
                JobQuantity > 0)
            {
                fieldsHaveEntries = true;
            }
            else
                fieldsHaveEntries = false;
            return fieldsHaveEntries;
        }

        public void ClearAllFields()
        {
            SelectedPartNumber = AvailablePartList[0]; 
            SelectedMoldNumber = AvailableMoldList[0];
            SelectedPressNumber = AvailablePressList[0];
            JobQuantity = 0;
            AdditionalNotes = "";
        }

        private void AddNewJobToDatabase()
        {
            if (AllFieldsHaveEntries() == false)
            {
                MessageBox.Show("All fields must have entries, with the exception of the notes field.", "Job Entry Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (AllFieldsHaveEntries() == true)
            {
                Job newJob = Job.GenerateNewJob(
                    JobQuantity,
                    AdditionalNotes);

                var partToAdd = _context.Parts.FirstOrDefault(p => p.PartNumber == SelectedPartNumber.PartNumber);
                var moldToAdd = _context.Molds.FirstOrDefault(m => m.MoldNumber == SelectedMoldNumber.MoldNumber);
                var pressToAdd = _context.Presses.FirstOrDefault(x => x.PressNumber == SelectedPressNumber.PressNumber);

                newJob.Part = _context.Parts.Attach(partToAdd);
                newJob.Mold = _context.Molds.Attach(moldToAdd);
                newJob.Press = _context.Presses.Attach(pressToAdd);

                _context.Jobs.Add(newJob);

                Job.AssignJobNumber(newJob);

                _context.SaveChanges();
                ClearAllFields();
                MessageBox.Show("Job Added Successfully!", "Job Added", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }   
}
