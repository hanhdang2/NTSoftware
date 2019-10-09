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

        DetailUserViewModel GetById(Guid id);

        #endregion GET

        #region POST

        DetailUser Add(DetailUserViewModel vm);

        #endregion POST

        #region PUT

        void Update(DetailUserViewModel vm);

        #endregion PUT

        #region DELETE

        void Delete(Guid id);

        #endregion DELETE

    }
}
