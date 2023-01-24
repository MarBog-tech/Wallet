using System.ComponentModel.DataAnnotations;

namespace Wallet.Domain.ViewModel.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Вкажіть ім'я")]
    [MaxLength(20, ErrorMessage = "Ім'я повинно мати довжину менше 20 символів")]
    [MinLength(2, ErrorMessage = "Ім'я повинно бути довше 2 символів")]
    public string? FirstName { get; set; }
    
    [MaxLength(20, ErrorMessage = "Прізвище повинно мати довжину менше 20 символів")]
    [MinLength(2, ErrorMessage = "Прізвище повинно бути довше 2 символів")]
    public string? LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Вкажіть електронну пошту")]
    public string? EmailLogin { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Вкажіть пароль")]
    [MinLength(8, ErrorMessage = "Пароль повинен бути не менше 8 символів")]
    public string? Password { get; set; }
}