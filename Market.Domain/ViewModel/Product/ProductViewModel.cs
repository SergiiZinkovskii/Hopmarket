using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Market.Domain.ViewModels.Product

{
    public class ProductViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Введіть назву")]
        [MinLength(2, ErrorMessage = "Мінімальна довжина має бути більша ніж два символи")]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Введіть опис")]
        public string Description { get; set; }

        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Вкажить модель")]
        [MinLength(2, ErrorMessage = "Мінімальна довжина має бути більша ніж два символи")]
        public string Model { get; set; }

        [Display(Name = "Потужність")]
        
        [Range(0, 10, ErrorMessage = "Довжина має бути в діапазоні від 0 до 600")]
        public double Power { get; set; }

        [Display(Name = "Вартість")]
        [Required(ErrorMessage = "Вкажіть вартість")]
        public decimal Price { get; set; }

        public string DateCreate { get; set; }

        [Display(Name = "Тип товару")]
        [Required(ErrorMessage = "Оберіть тип")]
        public string TypeProduct { get; set; }

        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }
    }
}