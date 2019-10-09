using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public interface IRuleService
    {
        #region GET

        RuleViewModel GetById(int id);
        PagedResult<RuleViewModel> GetAllPaging(int page, int pageSize, int companyId);

        #endregion GET

        #region POST

        Rule Add(RuleViewModel vm);

        #endregion POST

        #region PUT

        void Update(RuleViewModel vm);

        #endregion PUT

        #region DELETE

        void Delete(int id);

        #endregion DELETE
    }
}