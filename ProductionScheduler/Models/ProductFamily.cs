using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Models
{
    class ProductFamily
    {
        public int Id { get; set; }
        public IList<Part> Parts { get; set; }
    }
}
