using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionScheduler.Interfaces;

namespace ProductionScheduler.Models
{
    class Job : IJobCreator
    {

        public Job(
            int id, 
            Part part, 
            Press press, 
            Mold mold)
        {
            Id = id;
            Part = part;
            Press = press;
            Mold = mold;
        }

        public int Id { get; set; }
        public Press Press { get; set; }
        public Part Part { get; set; }
        public Mold Mold { get; set; }
        public int Quantity { get; set; }
        public string JobNotes { get; set; }
        public bool IsComplete { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Job GenerateNewJob(
               int id,
               Part part,
               Press press,
               Mold mold)
        {
            Job job = new Job(
                id,
                part,
                press,
                mold);

            return job;
        }

    }
}
