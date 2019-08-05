using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Interfaces
{
    interface ITextValidation
    {
        void ClearAllFields();
        bool AllFieldsHaveEntries();
    }
}
