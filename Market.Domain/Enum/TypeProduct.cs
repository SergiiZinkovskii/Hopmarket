using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enum
{
    public enum TypeProduct
    {
        [Display(Name = "Військове спорядження")]
        MilitaryEquipment = 0,
        [Display(Name = "Посуд")]
        Dishes = 1,
        [Display(Name = "Товари для дому")]
        AppliancesForHome = 2,
        [Display(Name = "Одяг")]
        Clothes = 3,
        [Display(Name = "Електроприлади")]
        ElectricalAppliances = 4,
        [Display(Name = "Краса та здоров'я")]
        BeautyAndHealth = 5,
    }
}