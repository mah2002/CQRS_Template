
namespace NecoTemplate.gRPC;

public interface IAuthService
{
    Task<bool?> GetAuthAsync(Guid id);
}
