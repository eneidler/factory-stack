using System.Collections.Generic;

namespace ProductionScheduler.Models {
    public class Press {
        public int Id { get; set; }
        public string PressNumber { get; set; }
        public string PressCapacity { get; set; }
        public IList<Mold> Molds { get; set; }
    }
}
