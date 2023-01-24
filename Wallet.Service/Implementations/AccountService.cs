using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wallet.Domain.Entity;
using Wallet.Domain.Enum;
using Wallet.Domain.Helpers;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Account;
using Wallet.Service.Interfaces;
using WalletDAL.Interfaces;

namespace Wallet.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ILogger<AccountService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IBaseResponse<User>> Register(RegisterViewModel model)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.EmailLogin == model.EmailLogin);
            if (user != null)
            {
                return new BaseResponse<User>()
                {
                    Description = "Користувач з такою електронною поштою вже існує",
                    StatusCode = StatusCode.UserAlreadyExists
                };
            }

            user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailLogin = model.EmailLogin,
                Password = HashPasswordHelper.HashPassword(model.Password),
            };

            await _unitOfWork.User.Create(user);
            await _unitOfWork.Save();

            return new BaseResponse<User>()
            {
                Data = user,
                Description = "Користувача додано",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[AccountService.Register]: error: {ex.Message}");
            return new BaseResponse<User>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<User>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.EmailLogin == model.EmailLogin);
            if (user == null)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Користувача не знайдено"
                };
            }

            if (user.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.WrongPassword,
                    Description = "Невірне значення логіну або паролю"
                };
            }

            return new BaseResponse<User>()
            {
                Data = user,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<User>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.EmailLogin == model.Login);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Користувача не знайдено"
                };
            }

            user.Password = HashPasswordHelper.HashPassword(model.NewPassword);
            await _unitOfWork.User.Update(user);
            await _unitOfWork.Save();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Ok,
                Description = "Пароль обновлено"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"[AccountService.ChangePassword] error: {ex.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<BaseResponse<bool>> ChangeUserSetting(ChangeUserSettingViewModel model)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.EmailLogin == model.EmailLogin);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Користувача не знайдено"
                };
            }

            if (model.FirstName != null)
                user.FirstName = model.FirstName;
            
            if (model.LastName != null)
                user.LastName = model.LastName;
            
            if (model.EmailLogin != null)
                user.EmailLogin = model.EmailLogin;
            
            await _unitOfWork.User.Update(user);
            await _unitOfWork.Save();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Ok,
                Description = "Дані користувача обновлено"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"[AccountService.ChangeUserSetting] error: {ex.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteUser(long id)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Data = false
                };
            }

            await _unitOfWork.User.Delete(user);
            await _unitOfWork.Save();
            _logger.LogInformation($"[AccountService.DeleteUser] користувача видалено");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.Ok,
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"[AccountService.DeleteUser] error: {ex.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутрішня помилка: {ex.Message}"
            };
        }
    }
    

}