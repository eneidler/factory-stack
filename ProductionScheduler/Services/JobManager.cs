using ProductionScheduler.Models;
using ProductionScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Services
{
    public sealed class JobManager
    {

        private static readonly object _padlock = new object();
        private static JobManager _instance = null;
        private IList<Job> _activeJobList;

        private JobManager()
        {

        }

        public static JobManager Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new JobManager();
                    }
                    return _instance;
                }
            }
        }

        public IList<Job> ActiveJobList
        {
            get
            {
                using(ProductionSchedulerContext context = new ProductionSchedulerContext())
                {
                    var jobList = context.Jobs.Where(j => j.IsComplete == false);

                    _activeJobList = jobList.ToList();
                }

                return _activeJobList;
            }
        }
    }
}
