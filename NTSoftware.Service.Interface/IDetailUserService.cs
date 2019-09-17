using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
    public interface IDetailUserService
    {
        #region GET

        DetailUserViewModel GetById(int id);
        List<DetailUserViewModel> GetAll();
        PagedResult<DetailUserViewModel> GetAllPaging(int page, int pageSize, string name, string phonenumber);

        #endregion GET

        #region POST

        DetailUser Add(DetailUserViewModel vm);

        #endregion POST

        #region PUT

        void Update(DetailUserViewModel vm);

        #endregion PUT



    }
}
