using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Linq;

namespace NTSoftware.Repository.Repository
{
    public class ContractCompanyRepository : NTRepository<ContractCompany, int>, IContractCompanyRepository
    {
        private AppDbContext _appContext;
        public ContractCompanyRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }

        public string GetLastestContractNumber(int companyId)
        {
            var lstContractCompany = FindAll(x => x.CompanyId == companyId).ToList();
            if (lstContractCompany.Count > 0)
            {
                var contractActive = lstContractCompany.Where(x => x.Status == Status.Active).SingleOrDefault();
                if (contractActive != null)
                {
                    return contractActive.ContractNumber;
                }
                var contractInActive = lstContractCompany.Where(x => x.Status == Status.Expired).OrderBy(x => x.UpdatedDate).LastOrDefault();
                if (contractInActive != null)
                {
                    return contractInActive.ContractNumber;
                }
                var contractNew = lstContractCompany.Where(x => x.Status == Status.New).OrderBy(x => x.CreatedDate).LastOrDefault();
                if (contractNew != null)
                {
                    return contractNew.ContractNumber;
                }
            }
            return null;
        }
    }
}
