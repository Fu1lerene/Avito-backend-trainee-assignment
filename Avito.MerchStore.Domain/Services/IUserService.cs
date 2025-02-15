using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories.Models;

namespace Avito.MerchStore.Domain.Services;

public interface IUserService
{
    Task<User?> GetUserByName(string username);
    Task<InfoResponse> GetInfo(string username);
}