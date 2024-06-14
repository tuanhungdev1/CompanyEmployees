using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts {
    public interface IEmployeeRepository {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);

        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        Task CreateEmployeeForCompanyAsync(Guid companyId, Employee employee);

        Task DeleteEmployeeAsync(Employee employee);
    }
}
