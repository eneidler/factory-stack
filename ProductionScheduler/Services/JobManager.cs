using ProductionScheduler.Models;
using ProductionScheduler.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductionScheduler.Services {
    public sealed class JobManager : BaseViewModel {

        private static readonly object _padlock = new object();
        private static JobManager _instance = null;
        private IList<Job> _activeJobList;

        private JobManager() {

        }

        public static JobManager Instance {
            get {
                lock (_padlock) {
                    if (_instance == null) {
                        _instance = new JobManager();
                    }
                    return _instance;
                }
            }
        }

        public IList<Job> ActiveJobList {
            get {
                using (ProductionSchedulerContext context = new ProductionSchedulerContext()) {
                    var jobList = context.Jobs.Where(j => j.IsComplete == false).Include(b => b.Part).Include(b => b.Mold).Include(b => b.Press);

                    _activeJobList = jobList.ToList();
                }

                return _activeJobList;
            }
            set {
                _activeJobList = value;
                NotifyOnPropertyChanged(nameof(ActiveJobList));
            }
        }
    }
}
