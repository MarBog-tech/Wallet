using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wallet.Domain.Entity;
using Wallet.Domain.Enum;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Transfers;
using Wallet.Domain.ViewModel.UserCard;
using Wallet.Service.Interfaces;
using WalletDAL.Interfaces;

namespace Wallet.Service.Implementations;

public class UserCardService : IUserCardService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserCardService(ILogger<AccountService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IBaseResponse<UserCard>> Create(UserCardViewModel model)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.EmailLogin == model.EmailLogin);
            if (user == null)
            {
                return new BaseResponse<UserCard>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Користувача не знайдено"
                };
            }

            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Number == model.Number);
            if (usercard != null)
            {
                new BaseResponse<UserCard>()
                {
                    StatusCode = StatusCode.CardAlreadyExists,
                    Description = "Карта з даним номером вже існує"  
                };
            }

            var newUsercard = new UserCard()
            {
                Number = model.Number,
                Description = model.Description,
                UserId = user.Id
            };
            await _unitOfWork.UserCard.Create(newUsercard);
            await _unitOfWork.Save();

            return new BaseResponse<UserCard>()
            {
                Description = "Карту створено",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserCard>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteUserCard(int id)
    {
        try
        {
            var usercard = _unitOfWork.UserCard.GetAll()
                .Select(x=> x.User.UserCards.FirstOrDefault(y=> y.Id == id))
                .FirstOrDefault();
            if (usercard == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Картку користувача не знайдена"
                };
            }

            await _unitOfWork.UserCard.Delete(usercard);
            await _unitOfWork.Save();
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.Ok,
                Description = "Карту видалено"
            };

        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.Ok
            };
        }
    }

    public async Task<IBaseResponse<bool>> EditUserCard(UserCardViewModel model)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Number == model.Number);
            if (usercard == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Карту не знайдено"
                };
            }

            usercard.Description = model.Description;
            await _unitOfWork.UserCard.Update(usercard);
            await _unitOfWork.Save();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Ok,
                Description = "Опис карти обновлено"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"[EditUserCard] error: {ex.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<IBaseResponse<List<UserCard>>> GetAllUserCard(int id)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().Include(x=> x.UserCards)
                .FirstOrDefaultAsync(x=> x.Id == id);
            if (user == null)
            {
                return new BaseResponse<List<UserCard>>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Користувача не знайдено"
                };
            }

            var usercards = user.UserCards.ToList();
            
            if (!usercards.Any())
            {
                return new BaseResponse<List<UserCard>>()
                {
                    Description = "Знайдено 0 елементів",
                    StatusCode = StatusCode.Ok
                };
            }
            
            return new BaseResponse<List<UserCard>>()
            {
                Data = usercards,
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception ex)
        {
            return new BaseResponse<List<UserCard>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }

    }

    public async Task<IBaseResponse<bool>> TransferFromCardInCard(decimal value, int id, int number)
    {
        try
        {
            var usercardfrom = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (usercardfrom == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Карту не знайдено"
                };
            }
            var usercardon = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Number == number);
            if (usercardon == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Карту отримувача не знайдено"
                };
            }

            var profit = new Profit()
            {
                Description = TypeProfit.TransfersOnTheCard,
                Value = value,
                Time = DateTime.Now,
                UserCardId = usercardon.Id
            };
            
            var expenses = new Expenses()
            {
                Description = TypeExpenses.TransfersFromTheCard,
                Value = value,
                Time = DateTime.Now,
                UserCardId = usercardfrom.Id
            };

            usercardfrom.Balance -= value;
            usercardon.Balance += value;
            await _unitOfWork.UserCard.Update(usercardon);
            await _unitOfWork.UserCard.Update(usercardfrom);
            await _unitOfWork.Profit.Create(profit);
            await _unitOfWork.Expenses.Create(expenses);
            await _unitOfWork.Save();

            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.Ok,
                Data = true,
                Description = "Переказ пройшов успішно"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}