using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.Enum;

public enum TypeProfit
{
    [Display(Name = "Зарплата")]
    JobSalary = 0,
    [Display(Name = "Підробіток")]
    SideJobSalary = 1,
    [Display(Name = "Перекази на карту")]
    TransfersOnTheCard = 2
}