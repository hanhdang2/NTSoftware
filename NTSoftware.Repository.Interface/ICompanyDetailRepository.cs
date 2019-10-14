using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Interface
{
    public interface ICompanyRepository : IRepository<CompanyDetail, int>
    {
        string GenCompanyCode(string companyName);
    }
}
