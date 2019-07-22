using ProductionScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Interfaces
{
    interface IJobCreator
    {

        Job GenerateNewJob(
            int id,
            Part part,
            Press press,
            Mold mold);

    }
}
