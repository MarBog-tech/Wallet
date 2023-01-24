using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wallet.Domain.ViewModel.Account;

public class ChangeUserSettingViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Вкажіть електронну пошту")]
    public string? EmailLogin { get; set; }
    
    [Required(ErrorMessage = "Вкажіть ім'я")]
    [MaxLength(20, ErrorMessage = "Ім'я повинно мати довжину менше 20 символів")]
    [MinLength(2, ErrorMessage = "Ім'я повинно бути довше 2 символів")]
    public string? FirstName { get; set; }
    
    [MaxLength(20, ErrorMessage = "Прізвище повинно мати довжину менше 20 символів")]
    [MinLength(2, ErrorMessage = "Прізвище повинно бути довше 2 символів")]
    public string? LastName { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Вкажіть нову електронну пошту")]
    public string? NewEmailLogin { get; set; }
}