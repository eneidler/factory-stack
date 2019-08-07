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

        private Part _selectedPartNumber;
        private ObservableCollection<Mold> _moldNumberList = new ObservableCollection<Mold>();
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

        public ObservableCollection<Mold> MoldNumberList
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


        private ObservableCollection<Mold> PopulateMoldList()
        {

            IList<Mold> usableMolds = SelectedPartNumber.Molds;
                       
            return (ObservableCollection<Mold>)usableMolds;
        }   

    }

    
}
