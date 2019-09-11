using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
    public interface IAdminService
    {
        AdminViewModel GetById(int id);
        List<AdminViewModel> GetAll();
        PagedResult<AdminViewModel> GetAllPaging(int page, int pageSize);
        Admin Add(AdminViewModel vm);
        void Update(AdminViewModel vm);
    }
}
