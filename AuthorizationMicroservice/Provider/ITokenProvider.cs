using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.Provider;

public interface ITokenProvider
{
    public string GenerateJWTToken(User user);
    public List<User> Users();
}
