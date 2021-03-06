﻿using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeContract : DomainEntity<int>
    {
        public string ContractNumber { set; get; }
        public string EmailRepresentativeA { set; get; }
        public string RepresentativeNameA { set; get; }
        public string PositionRepresentativeA { set; get; }
        public string EmailRepresentativeB { set; get; }
        public string RepresentativeNameB { set; get; }
        [Column(TypeName = "text")]
        public string ContentRule { get; set; }
        public Guid EmployeeId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int CompanyId { set; get; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryContract { get; set; }
    }
}
