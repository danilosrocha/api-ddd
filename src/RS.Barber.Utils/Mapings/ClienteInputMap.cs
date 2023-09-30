using AutoMapper;
using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Entities;

namespace RS.Barber.Utils.Mapings
{
    public class ClienteInputMap : Profile
    {
        public ClienteInputMap()
        {
            CreateMap<ClienteInput, Cliente>();
            CreateMap<Cliente, ClienteInput>();
        }
    }
}
