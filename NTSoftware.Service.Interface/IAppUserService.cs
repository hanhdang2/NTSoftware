using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service.Interface
{
   public interface IAppUserService
    {
        #region GET
        Task<AppUserViewModel> GetById(string id);
        Task<List<AppUserViewModel>> GetAllAsync();
        PagedResult<AppUserViewModel> GetAllPaging(int page, int pageSize);
        Task<bool> AddAsync(AppUserViewModel Vm);
        void UpdateAsync(AppUserViewModel userVm);

        #endregion GET
    }
}
