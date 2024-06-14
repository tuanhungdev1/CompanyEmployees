﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects {
    public record CompanyForCreationDto {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Address is 100 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Country is 50 characters.")]
        public string? Country { get; init; }

        [Required(ErrorMessage = "Employees is a required field.")]
        [MinLength(1, ErrorMessage = "A company must have at least one employee.")]
        public IEnumerable<EmployeeForCreationDto> Employees { get; init; }
    }

}
