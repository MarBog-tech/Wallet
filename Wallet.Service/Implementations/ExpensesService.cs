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

public class ExpensesService : IExpensesService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ExpensesService(ILogger<AccountService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IBaseResponse<Expenses>> Create(TransfersViewModel model, int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<Expenses>()
                {
                    StatusCode = StatusCode.UserCardNotFound,
                    Description = "Карту не знайдено"
                };
            }

            var expenses = new Expenses()
            {
                Description = (TypeExpenses)Convert.ToInt32(model.Description),
                Value = model.Value,
                UserCardId = usercard.Id,
                Time = DateTime.Now
            };

            usercard.Balance -= model.Value;
            await _unitOfWork.UserCard.Update(usercard);
            await _unitOfWork.Expenses.Create(expenses);
            await _unitOfWork.Save();

            return new BaseResponse<Expenses>()
            {
                StatusCode = StatusCode.Ok,
                Data = expenses
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Expenses>()
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
            var types = ((TypeExpenses[]) Enum.GetValues(typeof(TypeExpenses)))
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

    public async Task<IBaseResponse<List<Expenses>>> GetAllCardExpenses(int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<List<Expenses>>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Карту не знайдено"
                };
            }
            var expenses = _unitOfWork.Expenses.GetAll().Where(x=>x.UserCardId == id).ToList();

            if (!expenses.Any())
            {
                return new BaseResponse<List<Expenses>>()
                {
                    Description = "Знайдено 0 елементів",
                    StatusCode = StatusCode.Ok
                };
            }
            
            return new BaseResponse<List<Expenses>>()
            {
                Data = expenses,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Expenses>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<List<Expenses>>> GetExpensesByType(string type, int id)
    {
        try
        {
            var usercard = await _unitOfWork.UserCard.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
            if (usercard == null)
            {
                return new BaseResponse<List<Expenses>>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Карту не знайдено"
                };
            }

            
            var expenses = usercard.Expenses.Where(x => x.Description == (TypeExpenses)Convert.ToInt32(type)).ToList();

            if (!expenses.Any())
            {
                return new BaseResponse<List<Expenses>>()
                {
                    Description = "Знайдено 0 елементів",
                    StatusCode = StatusCode.Ok
                };
            }
            
            return new BaseResponse<List<Expenses>>()
            {
                Data = expenses,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Expenses>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}