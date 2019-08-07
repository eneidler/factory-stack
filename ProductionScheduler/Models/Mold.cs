using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Models
{
    class Mold
    {

        public Mold()
        {
        }

        public Mold(string moldNumber)
        {
            MoldNumber = moldNumber;
        }


        public int Id { get; set; }
        public string MoldNumber { get; set; }
        public int NumberOfCavities { get; set; }
        public IList<Press> Presses { get; set; }
        public IList<Part> Parts { get; set; }


    }

}
