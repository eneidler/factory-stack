using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionScheduler.Interfaces;
using ProductionScheduler.Services;

namespace ProductionScheduler.Models
{
    class Job //: IJobCreator
    {
     
        public Job()
        {

        }

        public int Id { get; set; }
        public int JobNumber { get; set; }
        public Press Press { get; set; }
        public Part Part { get; set; }
        public Mold Mold { get; set; }
        public int Quantity { get; set; }
        public string JobNotes { get; set; }
        public bool IsComplete { get; set; }
        public bool IsPaused { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public static Job GenerateNewJob(
               int quantity,
               string jobNotes)
        {
            Job job = new Job()
            {
                Part = new Part(),
                Mold = new Mold(),
                Press = new Press(),
                Quantity = quantity,
                JobNotes = jobNotes,
                IsComplete = false,
                IsPaused = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10)
            };

            return job;
        }

        public static int AssignJobNumber()
        {
            ProductionSchedulerContext context = new ProductionSchedulerContext();
            
            int searchJobNumber = context.Jobs.Max(j => j.JobNumber);

            int newJobNumber = searchJobNumber + 1;
            
            return newJobNumber;
        }

    }
}
