using System.Collections.Generic;

namespace ProductionCode.MockingExample
{
    public interface IEmployeeService
    {
        IEnumerable<IEmployee> GetEmployeesInNewYorkOffice();
    }
}