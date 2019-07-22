using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Models
{
    class Press
    {
        public int Id { get; set; }
        public string PressNumber { get; set; }
        public string PressCapacity { get; set; }
        public IList<Mold> Molds { get; set; }
    }
}
