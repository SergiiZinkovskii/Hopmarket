using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть ім'я")]
        [MaxLength(20, ErrorMessage = "Ім'я має складатися меньше ніж з 20 символів")]
        [MinLength(2, ErrorMessage = "Ім'я має складатися більше ніж з одного символу")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(6, ErrorMessage = "Пароль має складатися не меньше ніж з 6 символів")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Підтвердіть  пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string PasswordConfirm { get; set; }
    }
}