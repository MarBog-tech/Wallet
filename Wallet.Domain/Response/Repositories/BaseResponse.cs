using Wallet.Domain.Enum;
using Wallet.Domain.Response.Interfaces;

namespace Wallet.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    
    public StatusCode StatusCode { get; set; }

    public T Data { get; set; }
}