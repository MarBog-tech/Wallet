using Wallet.Domain.Entity;
using Wallet.Domain.Response;
using Wallet.Domain.Response.Interfaces;
using Wallet.Domain.ViewModel.UserCard;

namespace Wallet.Service.Interfaces;

public interface IUserCardService
{
    Task<IBaseResponse<UserCard>> Create(UserCardViewModel model);
    Task<IBaseResponse<bool>> DeleteUserCard(int id);
    Task<IBaseResponse<bool>> EditUserCard(UserCardViewModel model);
    Task<IBaseResponse<List<UserCard>>> GetAllUserCard(int id);
    
    Task<IBaseResponse<bool>> TransferFromCardInCard(decimal value, int id, int number);
}