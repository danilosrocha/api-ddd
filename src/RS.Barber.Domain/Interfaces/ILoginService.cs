using RS.Barber.Domain.Entities;

namespace RS.Barber.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(Usuario input);
    }
}
