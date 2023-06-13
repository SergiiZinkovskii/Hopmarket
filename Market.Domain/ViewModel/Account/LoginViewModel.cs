using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть ім'я")]
        [MaxLength(20, ErrorMessage = "Ім'я має складатися меньше ніж з 20 символів")]
        [MinLength(2, ErrorMessage = "Ім'я має складатися більше ніж з одного символу")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}