using RS.Barber.Domain.Entities;

namespace RS.Barber.Domain.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario input);
    }
}
