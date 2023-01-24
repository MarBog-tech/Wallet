using Wallet.Domain.Entity;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.Transfers;

namespace Wallet.Service.Interfaces;

public interface IProfitService
{
    Task<IBaseResponse<Profit>> Create(TransfersViewModel model, int id);
    BaseResponse<Dictionary<int, string>> GetTypes();
    Task<IBaseResponse<List<Profit>>> GetAllCardProfit(int id);
    Task<IBaseResponse<List<Profit>>> GetProfitByType(string type, int id);
}