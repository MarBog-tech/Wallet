using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.ViewModel.Account;

public class ChangePasswordViewModel
{
    
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Вкажіть електронну пошту")]
    public string? Login { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Вкажіть старий пароль")]
    [MinLength(8, ErrorMessage = "Пароль повинен бути не менше 8 символів")]
    public string? OldPassword { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Вкажіть новий пароль")]
    [MinLength(8, ErrorMessage = "Пароль повинен бути не менше 8 символів")]
    public string? NewPassword { get; set; }
}