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

        private string _selectedPartNumber;
        private ObservableCollection<string> _moldNumberList = new ObservableCollection<string>();
        private string _selectedMoldNumber;
        private ObservableCollection<string> _pressNumberList = new ObservableCollection<string>();
        private string _selectedPressNumber;

        public AddJobViewModel()
        {

        }

        public string SelectedPartNumber
        {
            get => _selectedPartNumber;
            set
            {
                _selectedPartNumber = value;
                NotifyOnPropertyChanged(nameof(SelectedPartNumber));
                NotifyOnPropertyChanged(nameof(MoldNumberList));
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

        public ObservableCollection<string> MoldNumberList
        {
            get => _moldNumberList = PopulateMoldList();
            set
            {
                _moldNumberList = value;
                NotifyOnPropertyChanged(nameof(SelectedPartNumber));
                NotifyOnPropertyChanged(nameof(SelectedMoldNumber));
                NotifyOnPropertyChanged(nameof(MoldNumberList));
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


        private ObservableCollection<string> PopulateMoldList()
        {

            List<Mold> usableMolds = new List<Mold>();
            ObservableCollection<string> usableMoldNumbers = new ObservableCollection<string>();

            using (var context = new ProductionSchedulerContext())
            {
                /*var query = from p in context.Parts
                            where p.PartNumber == SelectedPartNumber
                            select p;

                var partNumber = query.SingleOrDefault();*/

                var defaultQuery = from p in context.Parts
                                 select p;

                var partNumber = defaultQuery.FirstOrDefault();               

                usableMolds = partNumber.Molds.ToList();

                    foreach (Mold mold in usableMolds)
                    {
                        usableMoldNumbers.Add(mold.MoldNumber);
                    }               
            }
            return usableMoldNumbers;
        }   

    }

    
}
