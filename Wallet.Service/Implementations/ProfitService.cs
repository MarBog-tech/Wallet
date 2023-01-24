using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wallet.Domain.Entity;
using Wallet.Domain.Enum;
using Wallet.Domain.Extensions;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Transfers;
using Wallet.Service.Interfaces;
using WalletDAL.Interfaces;

namespace Wallet.Service.Implementations;

public class ProfitService : IProfitService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProfitService(ILogger<AccountService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<IBaseResponse<Profit>> Create(TransfersViewModel model, int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<Profit>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Карту не знайдено"
                };
            }

            var profit = new Profit()
            {
                Description = (TypeProfit)Convert.ToInt32(model.Description),
                Value = model.Value,
                UserCardId = usercard.Id,
                Time = DateTime.Now
            };

            usercard.Balance += model.Value;
            await _unitOfWork.UserCard.Update(usercard);
            await _unitOfWork.Profit.Create(profit);
            await _unitOfWork.Save();

            return new BaseResponse<Profit>()
            {
                StatusCode = StatusCode.Ok,
                Data = profit
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Profit>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public BaseResponse<Dictionary<int, string>> GetTypes()
    {
        try
        {
            var types = ((TypeProfit[]) Enum.GetValues(typeof(TypeProfit)))
                .ToDictionary(k => (int) k, t => t.GetDisplayName());

            return new BaseResponse<Dictionary<int, string>>()
            {
                Data = types,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<int, string>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<List<Profit>>> GetAllCardProfit(int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<List<Profit>>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Карту не знайдено"
                };
            }
            var profits = _unitOfWork.Profit.GetAll().Where(x=>x.UserCardId == id).ToList();

            if (!profits.Any())
            {
                return new BaseResponse<List<Profit>>()
                {
                    Description = "Знайдено 0 елементів",
                    StatusCode = StatusCode.Ok
                };
            }
            
            return new BaseResponse<List<Profit>>()
            {
                Data = profits,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Profit>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<List<Profit>>> GetProfitByType(string type, int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<List<Profit>>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Карту не знайдено"
                };
            }

            
            var profits = usercard.Profits.Where(x => x.Description == (TypeProfit)Convert.ToInt32(type)).ToList();

            if (!profits.Any())
            {
                return new BaseResponse<List<Profit>>()
                {
                    Description = "Знайдено 0 елементів",
                    StatusCode = StatusCode.Ok
                };
            }
            
            return new BaseResponse<List<Profit>>()
            {
                Data = profits,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Profit>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}