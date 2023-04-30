using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Пароль має складатися більше ніж з п'яти символів")]
        public string NewPassword { get; set; }
    }
}