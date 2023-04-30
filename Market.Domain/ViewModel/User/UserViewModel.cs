using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Вкажіть роль")]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Вкажіть логін")]
        [Display(Name = "Логін")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}