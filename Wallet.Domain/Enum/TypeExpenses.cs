using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.Enum;

public enum TypeExpenses
{
    [Display(Name = "Їжа та напої")]
    Food = 0,
    [Display(Name = "Комунальні послуги")]
    UtilityServices = 1,
    [Display(Name = "Перекази з карти")]
    TransfersFromTheCard = 2,
    [Display(Name = "Розваги")]
    Entertainment = 3,
    [Display(Name = "Транспорт")]
    Transport = 4,
    [Display(Name = "Оренда")]
    Rent = 5
}