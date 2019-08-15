using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Interfaces;

namespace View.Services
{
    public class TimeService : ITimeService
    {
        public DateTimeOffset Now
        {
            get
            {
                return DateTimeOffset.Now;
            }
        }
    }
}
