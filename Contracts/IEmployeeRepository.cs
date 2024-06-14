using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts {
    public interface IEmployeeRepository {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);

        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        Task CreateEmployeeForCompanyAsync(Guid companyId, Employee employee);

        Task DeleteEmployeeAsync(Employee employee);
    }
}
