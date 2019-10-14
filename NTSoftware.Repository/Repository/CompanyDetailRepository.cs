using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NTSoftware.Repository.Repository
{
    public class CompanyRepository :NTRepository<CompanyDetail, int>,ICompanyRepository
    {
        private AppDbContext _appContext;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }

        public string GenCompanyCode(string companyName)
        {
            var newName = Regex.Replace(companyName, @"[^0-9a-zA-Z]+", " ");
            var nameSplit = newName.Split(' ');
            if(nameSplit.Length == 1)
            {
                return nameSplit[0].Substring(0,1).ToUpper();
            }
            else
            {
                return $"{nameSplit[0].Substring(0, 1).ToUpper()}{nameSplit[1].Substring(0, 1).ToUpper()}";
            }
        }
    }
}
