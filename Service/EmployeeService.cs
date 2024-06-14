﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service {
    internal sealed class EmployeeService : IEmployeeService {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeesWithMetaData = await _repository.Employee
        .GetEmployeesAsync(companyId, employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employeeDb is null)
                throw new EmployeeNotFoundException(id);

            var employeeDto = _mapper.Map<EmployeeDto>(employeeDb);

            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            await _repository.Employee.CreateEmployeeForCompanyAsync(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(id);

            await _repository.Employee.DeleteEmployeeAsync(employeeForCompany);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);

            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);

            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges) {
            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);

            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }
    }
}
