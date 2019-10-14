using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Interface
{
    public interface IDetailUserRepository : IRepository<DetailUser, Guid>
    {
        List<UserSearchViewModel> GetLstSearch(List<UserSearchViewModel> lstSelected, int companyId, string keyword);
    }
}
