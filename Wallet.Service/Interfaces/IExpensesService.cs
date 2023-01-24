using Wallet.Domain.Entity;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Transfers;

namespace Wallet.Service.Interfaces;

public interface IExpensesService
{
    Task<IBaseResponse<Expenses>> Create(TransfersViewModel model, int id);
    BaseResponse<Dictionary<int, string>> GetTypes();
    Task<IBaseResponse<List<Expenses>>> GetAllCardExpenses(int id);
    Task<IBaseResponse<List<Expenses>>> GetExpensesByType(string type, int id);
}