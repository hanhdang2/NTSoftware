using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Core.Models.DomainEntity
{
    public class DomainEntity<T> : IDomainEntity
    {
        public T Id { get; set; }

        public bool IsTrantSient()
        {
            return Id.Equals(default(T));
        }

        [MaxLength(256)]
        public string CreatedBy { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int DeleteFlag { get; set; }
    }
}
