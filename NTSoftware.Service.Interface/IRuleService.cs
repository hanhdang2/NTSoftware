using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public interface IRuleService
    {
        RuleViewModel GetById(int id);
        List<RuleViewModel> GetAll();
        PagedResult<RuleViewModel> GetAllPaging(int page, int pageSize);
        Rule Add(RuleViewModel vm);
    }
}