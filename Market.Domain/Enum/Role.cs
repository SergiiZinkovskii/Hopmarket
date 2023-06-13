using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "user")]
        User = 0,
        [Display(Name = "moderator")]
        Moderator = 1,
        [Display(Name = "admin")]
        Admin = 2,
    }
}