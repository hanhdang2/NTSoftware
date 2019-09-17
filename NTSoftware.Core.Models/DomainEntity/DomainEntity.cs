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

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int DeleteFlag { get; set; }
    }
}
