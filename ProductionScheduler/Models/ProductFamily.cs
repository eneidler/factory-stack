using System.Collections.Generic;

namespace ProductionScheduler.Models {
    internal class ProductFamily {
        public int Id { get; set; }
        public string ProductFamilyCategory { get; set; }
        public IList<Part> Parts { get; set; }
    }
}
