using ProductionScheduler.Interfaces;
using ProductionScheduler.Models;
using ProductionScheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionScheduler.ViewModels
{
    class AddJobViewModel : BaseViewModel
    {

        ProductionSchedulerContext _context = new ProductionSchedulerContext();
        private Part _selectedPartNumber;
        private IList<Part> _availablePartList = new ObservableCollection<Part>();
        private IList<Mold> _availableMoldList = new ObservableCollection<Mold>();
        private Mold _selectedMoldNumber;
        private IList<Press> _availablePressList = new ObservableCollection<Press>();
        private Press _selectedPressNumber;
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

            }
        }


        public IList<Part> AvailablePartList
        {
            get
            {
                using (var context = new ProductionSchedulerContext())
                {
                    var query = from p in context.Parts
                                select p;

                    var usableParts = query.ToList();
                    _availablePartList = usableParts;
                }
                return _availablePartList;
            }
            set
            {
                _availablePartList = value;

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
            set
            {
                _availableMoldList = value;
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
            set
            {
                _availablePressList = value;

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

        private void AddNewJobToDatabase()
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
        }
    }   
}
