using System.ComponentModel.DataAnnotations;

namespace Market.Domain.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Кількість")]
        [Range(1, 100, ErrorMessage = "Кількість має бути від 1 до 100")]
        public int Quantity { get; set; }

        [Display(Name = "Дата створення")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Адреса доставки")]
        [Required(ErrorMessage = "Вкажіть адресу доставки")]
        [MinLength(5, ErrorMessage = "Адреса має містити більше 5 символів")]
        [MaxLength(200, ErrorMessage = "Адреса має быть меньше  200 символів")]
        public string Address { get; set; }

        [Display(Name = "м'я")]
        [Required(ErrorMessage = "Вкажіть ім'я")]
        [MaxLength(20, ErrorMessage = "Ім'я має складатися меньше ніж з  20 символів")]
        [MinLength(2, ErrorMessage = "Ім'я має складатися більше ніж з 1 символу")]
        public string FirstName { get; set; }

        [Display(Name = "Прізвище")]
        [MaxLength(50, ErrorMessage = "Прізвище має складатися меньше ніж з 50 символів")]
        [MinLength(2, ErrorMessage = "Прізвище має складатися меньше ніж з 50 символів")]
        public string LastName { get; set; }

        [Display(Name = "По батькові")]
        [MaxLength(50, ErrorMessage = "По батькові має складатися меньше ніж з 50 символів")]
        [MinLength(2, ErrorMessage = "По батькові должно иметь длину больше 2 символов")]
        public string MiddleName { get; set; }


        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Вкажіть номер телефону")]
        public int Phone { get; set; }

        [Display(Name = "Поштовий оператор")]
        [Required(ErrorMessage = "Оберіть поштового оператора")]
        public string Post { get; set; }

        [Display(Name = "Форма оплати")]
        [Required(ErrorMessage = "Оберіть вид оплати")]
        public string Payment { get; set; }

        [Display(Name = "Коментарі до замовлення")]
        [Required(ErrorMessage = "Додайте коментарі до замовлення")]
        public string Comments { get; set; }


        public long ProductId { get; set; }

        public string Login { get; set; }
    }
}