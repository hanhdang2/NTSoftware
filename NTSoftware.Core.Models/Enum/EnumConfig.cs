using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Core.Models.Enum
{
    public enum Gender
    {
        None,
        FeMale,
        Male
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [Display(Name = "Active")] Active = 1,
        [Display(Name = "Inactive")] Inactive = 2,
        [Display(Name = "Expired")] Expired = 3,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Roles
    {
        [Display(Name = "AdminNT")] AdminEF = 1,
        [Display(Name = "AdminCompany")] AdminCompany = 2,
        [Display(Name = "Employee")] Employee = 3,
    }
}
