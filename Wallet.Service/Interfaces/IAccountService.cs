using System.Security.Claims;
using Wallet.Domain.Entity;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Account;

namespace Wallet.Service.Interfaces;

public interface IAccountService
{
    Task<IBaseResponse<User>> Register(RegisterViewModel model);

    Task<IBaseResponse<User>> Login(LoginViewModel model);

    Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    
    Task<BaseResponse<bool>> ChangeUserSetting(ChangeUserSettingViewModel model);
    
    Task<IBaseResponse<bool>> DeleteUser(long id);
}