using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTSoftware.ViewModel.Auth
{
    public class ChangePasswordWithOldPasswordViewModel
    {
        [Required(ErrorMessage = "Email đăng nhập không được trống")]
        public string userName { get; set; }
        [Required(ErrorMessage ="Mật khẩu cũ không được trống")]
        public string oldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được trống")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[\W])(?!.*[""]).{8,}$", ErrorMessage = "Mật khẩu mới phải từ 8 ký tự, phải có ký tự thường, hoa, số và ký tự đặc biệt")]
        public string newPassword { get; set; }
    }
}
