using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.ViewModel.Transfers;

public class TransfersViewModel
{
    [Display(Name = "Тип транзакції")]
    [Required(ErrorMessage = "Виберіть тип")]
    public string? Description { get; set; }
    
    [Display(Name = "Кошти")]
    [Required(ErrorMessage = "Вкажіть кількість кошти для виконнання операції")]
    public decimal Value { get; set; }
}