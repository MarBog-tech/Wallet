using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.ViewModel.UserCard;

public abstract class UserCardViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Введіть електронну пошту")]
    public string? EmailLogin { get; set; }
    
    [Display(Name = "Номер карти")]
    [Required(ErrorMessage = "Вкажіть номер карти")]
    [Range(1, 9999, ErrorMessage = "Значення карти повинне бути від 0 до 10000")]
    public int Number { get; set; }
    
    [Display(Name = "Опис карти")]
    public string? Description { get; set; }
}