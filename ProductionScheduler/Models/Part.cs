using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Models
{
    class Part
    {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string ProductFamilyCategory { get; set; }
        public string ProductDescription { get; set; }
        public int CureTimeInMinutes { get; set; }
        public IList<Mold> Molds { get; set; }
    }
}
