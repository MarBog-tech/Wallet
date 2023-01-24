namespace Wallet.Domain.Enum;

public enum StatusCode
{
    UserNotFound = 0,
    UserAlreadyExists = 1,
    UserCardNotFound = 10,
    CardAlreadyExists = 15,
    WrongPassword = 30,
    Ok = 200,
    InternalServerError = 500
}