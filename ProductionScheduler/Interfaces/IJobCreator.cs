using ProductionScheduler.Models;

namespace ProductionScheduler.Interfaces {
    internal interface IJobCreator {

        Job GenerateNewJob(
            Part part,
            Press press,
            Mold mold,
            int quantity,
            string jobNotes);

    }
}
