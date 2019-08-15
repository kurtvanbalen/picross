using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using ViewModel.Interfaces;

namespace View.Services
{
    public class TimerService : ITimerService
    {
        private DispatcherTimer _timer;
        public event Action<ITimerService> Tick;

        public TimerService()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick?.Invoke(this);
        }

        public void Start(TimeSpan interval)
        {
            _timer.Interval = interval;
            if (!_timer.IsEnabled) _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
