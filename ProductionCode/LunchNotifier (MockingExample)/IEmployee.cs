using System;

namespace ProductionCode.MockingExample
{
    public interface IEmployee
    {
        bool IsWorkingOnDate(DateTime date);
        LunchNotifier.NotificationType GetNotificationPreference();
    }
}