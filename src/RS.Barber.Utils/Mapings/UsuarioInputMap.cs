using AutoMapper;
using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Models;

namespace RS.Barber.Utils.Mapings
{
    public class UsuarioInputMap : Profile
    {
        public UsuarioInputMap()
        {
            CreateMap<UsuarioInput, Usuario>();
            CreateMap<Usuario, UsuarioInput>();
        }

    }
}
