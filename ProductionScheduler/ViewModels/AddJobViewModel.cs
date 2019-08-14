using ProductionScheduler.Models;
using ProductionScheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.ViewModels
{
    class AddJobViewModel : BaseViewModel
    {

        ProductionSchedulerContext _context = new ProductionSchedulerContext();
        private Part _selectedPartNumber;
        private IList<Part> _availablePartList;
        private IList<Mold> _moldNumberList = new ObservableCollection<Mold>();
        private string _selectedMoldNumber;
        private ObservableCollection<string> _pressNumberList = new ObservableCollection<string>();
        private string _selectedPressNumber;

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
                NotifyOnPropertyChanged(nameof(MoldNumberList));
            }
        }

        public IList<Part> AvailablePartList
        {
            get
            {
                using(var context = new ProductionSchedulerContext())
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

        public string SelectedMoldNumber
        {
            get => _selectedMoldNumber;
            set
            {
                _selectedMoldNumber = value;
                NotifyOnPropertyChanged(nameof(SelectedMoldNumber));
            }
        }

        public string SelectedPressNumber
        {
            get => _selectedPressNumber;
            set
            {
                _selectedPressNumber = value;
                NotifyOnPropertyChanged(nameof(SelectedPressNumber));
            }
        }

        public IList<Mold> MoldNumberList
        {
            get
            {
                string selectedPartNumber = SelectedPartNumber.PartNumber;
                var moldList = _context.Molds.Where(m => m.Parts.Any(x => x.PartNumber.Contains(selectedPartNumber)));

                List<Mold> usableMolds = moldList.ToList();
 
                _moldNumberList = usableMolds;
                return _moldNumberList;
            }
            set
            {
                _moldNumberList = value;
                NotifyOnPropertyChanged(nameof(SelectedMoldNumber));

            }
        }

        public ObservableCollection<string> PressNumberList
        {
            get => _pressNumberList;
            set
            {
                _pressNumberList = value;
                NotifyOnPropertyChanged(nameof(SelectedPressNumber));
                NotifyOnPropertyChanged(nameof(PressNumberList));
            }
        }

        
        //TODO: Use a method for populating lists once the null issue is sorted for MoldNumberList & SelectedPartNumber.PartNumber
        //private ObservableCollection<Mold> PopulateMoldList()
        //{
        //    //ObservableCollection<Mold> moldsToList = new ObservableCollection<Mold>();
        //    IList<Mold> availableMolds = new List<Mold>();

        //    if(_selectedPartNumber.Molds == null)
        //    {
        //        Part firstPart = _context.Parts.FirstOrDefault(p => p.Molds == p.Molds[0]);
        //        availableMolds = firstPart.Molds;
        //    }

        //    if (_selectedPartNumber.Molds != null)
        //    {
        //        foreach (Mold mold in _selectedPartNumber.Molds)
        //        {
        //            availableMolds.Add(mold);
        //        }
        //    }


        //    if (availableMolds != null)
        //    {
        //        foreach (Mold mold in availableMolds)
        //        {
        //            Mold newMold = _context.Molds.FirstOrDefault(m => m.MoldNumber == mold.MoldNumber);
        //            moldsToList.Add(newMold);
        //        }
        //    }
        //    return (ObservableCollection<Mold>)availableMolds;
        //} 

    }

    
}
