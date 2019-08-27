using ProductionScheduler.Models;
using System;
using System.Collections.Generic;
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
            get => _activeJobList;
            set
            {
                _activeJobList = value;
            }
        }

    }
}
