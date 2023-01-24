using Wallet.Domain.Enum;

namespace Wallet.Domain.Response.Interfaces;

public interface IBaseResponse<T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; set; }
}