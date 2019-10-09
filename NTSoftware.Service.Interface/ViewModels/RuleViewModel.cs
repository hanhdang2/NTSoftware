using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class RuleViewModel
    {
        public int Id { get; set; }
        public string Content { set; get; }
        public string TypeContractName { get; set; }
        public int CompanyId { set; get; }
    }
}
