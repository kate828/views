using System;

namespace ViewTask.Services
{
    public class SimpleTimeService : ITimeService
    {
        public string GetTimeForTomorrow()
        {
            return DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
        }
    }
}