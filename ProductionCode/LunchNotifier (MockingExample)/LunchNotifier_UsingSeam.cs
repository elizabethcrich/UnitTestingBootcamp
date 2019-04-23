using System;

namespace ProductionCode.MockingExample
{
    public class LunchNotifier_UsingSeam 
    {
       
        private readonly INotificationService _notificationService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public LunchNotifier_UsingSeam(INotificationService notifySrv, IEmployeeService employeeSrv, ILogger logger)
        {
            _notificationService = notifySrv;
            _employeeService = employeeSrv;
            _logger = logger;
        }

       
        public virtual DateTime GetDateTime() => DateTime.Now;

        /// <summary>
        /// This method is identical to SendLunchtimeNotification, except that
        /// it extracts the use of System.DateTime.Now into a seperate, public and virtual
        /// method, so that constrained frameworks (Moq, RhinoMocks, NSubstitute) can mock
        /// the "current" time.
        /// </summary>
        public void SendLunchtimeNotifications()
        {
            var now = GetDateTime();
            var templateToUse = now.Hour > 12 ? LunchNotifier.LateLunchTemplate : LunchNotifier.RegularLunchTemplate;
            _logger.Write($"Using template: {templateToUse}");

            var nycEmployees = _employeeService.GetEmployeesInNewYorkOffice();

            foreach (var employee in nycEmployees)
            {
                if (!employee.IsWorkingOnDate(now.Date))
                {
                    _logger.Debug("Skipping employe {employee}");
                    continue;
                }

                try
                {
                    var notificationType = employee.GetNotificationPreference();
                    switch (notificationType)
                    {
                        case LunchNotifier.NotificationType.Email:
                            _notificationService.SendEmail(employee, templateToUse);
                            break;
                        case LunchNotifier.NotificationType.Slack:
                            _notificationService.SendSlackMessage(employee, templateToUse);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }


    }
}