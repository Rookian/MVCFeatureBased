using System;

namespace MVCFeatureBased.Web.Infrastructure
{
    public class Clock : IClock
    {
        public DateTime Now () => DateTime.Now;
    }

    public interface IClock
    {
        DateTime Now();
    }
}