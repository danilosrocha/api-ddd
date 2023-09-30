using RS.Barber.Domain.Models;

namespace RS.Barber.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(LoginInput input);
    }
}
