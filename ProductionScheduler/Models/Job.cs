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
        private const int _initializingJobNumber = 0;
        private const int _jobCounter = 1;
        private const int _firstTimeJobNumber = 1;

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
                Quantity = quantity,
                JobNotes = jobNotes,
                IsComplete = false,
                IsPaused = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                JobNumber = _initializingJobNumber
            };

            return job;
        }

        public static void AssignJobNumber(Job job)
        {
            using (ProductionSchedulerContext context = new ProductionSchedulerContext())
            {
                var storedJobCount = context.Jobs.Count(); //This is used to establish an initial job number for the first job entered, otherwise 'searchJobNumber' will return null and throw an exception.
                if (storedJobCount <= 0)
                    job.JobNumber = _firstTimeJobNumber;

                if (storedJobCount > 0)
                {
                    var searchJobNumber = context.Jobs.Max(j => j.JobNumber);

                    if (searchJobNumber <= _initializingJobNumber)
                    {
                        job.JobNumber = _firstTimeJobNumber;
                    }
                    if (searchJobNumber > _initializingJobNumber)
                    {
                        job.JobNumber = searchJobNumber + _jobCounter;
                    }
                }
            }
        }
    }
}
