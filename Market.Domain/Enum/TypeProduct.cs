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
        [Display(Name = "Мобільні аксесуари")]
        MobileAccessories = 6,
        [Display(Name = "Товари для дітей")]
        ForChild = 7,
        [Display(Name = "Ліхтарі")]
        Lamp = 8,
        [Display(Name = "Сумки, рюкзаки, ремені та інші аксесуари")]
        BagsBackpacksBelts = 9,
        [Display(Name = "Товари для туризму")]
        TourismProducts = 10,
        [Display(Name = "Засоби індивідуального захисту")]
        PersonalProtectiveEquipment = 11,
        [Display(Name = "Масажери")]
        Massagers = 12,
        [Display(Name = "М'ясорубки")]
        MeatGrinders = 13,
        [Display(Name = "Кухонні комбайни")]
        KitchenBlenders = 14,
        [Display(Name = "Камери спостереження")]
        SurveillanceCameras = 15,
        [Display(Name = "Відеореєстратори")]
        Dashcams = 16,
        [Display(Name = "Електробритви")]
        ElectricShavers = 17,
        [Display(Name = "Автомобільна акустика")]
        CarAudio = 18,
        [Display(Name = "Сувеніри")]
        Souvenirs = 19,
        [Display(Name = "Щипці та плойки для волосся")]
        CurlingIronsAndHairStraighteners = 20,
        [Display(Name = "Спорт, здоров'я, відпочинок")]
        SportsHealthLeisure = 21,
        [Display(Name = "Дитячі басейни")]
        ChildrensPools = 22,
        [Display(Name = "Електроніка")]
        Electronics = 23,
        [Display(Name = "Товари для дому та саду")]
        HomeAndGarden = 24,
        [Display(Name = "Ліхтарі, лампи, запальнички")]
        FlashlightsLampsLighters = 25,
        [Display(Name = "Спорт, фітнес, туризм")]
        SportsFitnessTourism = 26,
        [Display(Name = "Все для кухні")]
        EverythingForKitchen = 27,
        [Display(Name = "Автотовари")]
        AutoProducts = 28
    }
}