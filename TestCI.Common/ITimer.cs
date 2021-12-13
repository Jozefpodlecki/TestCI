using System;

namespace TestCI.Common
{
    public interface ITimer
    {
        void Start(TimeSpan dueTime, TimeSpan period, object state);

        void Start(TimeSpan dueTime, TimeSpan period);

        void Stop();

        event TimerEventHandler Tick;
    }
}
