using System.Collections.Generic;

namespace ProductionScheduler.Models {
    public class Mold {

        public int Id { get; set; }
        public string MoldNumber { get; set; }
        public int NumberOfCavities { get; set; }
        public IList<Press> Presses { get; set; }
        public IList<Part> Parts { get; set; }
    }

}
