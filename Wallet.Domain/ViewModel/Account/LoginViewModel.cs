using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.ViewModel.Account;

public class LoginViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Введіть електронну пошту")]
    public string? EmailLogin { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Вкажіть пароль")]
    [MinLength(8, ErrorMessage = "Пароль повинен бути не менше 8 символів")]
    public string? Password { get; set; }
}