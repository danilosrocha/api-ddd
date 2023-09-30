using AutoMapper;
using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Models;
using RS.Barber.Infra.Data.Repositories;

namespace RS.Barber.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> AdicionarUsuarioAsync(UsuarioInput input)
        {
            var usuario = _mapper.Map<Usuario>(input);

            var usuarioAlreadyExists = await (input.Email != null ? _usuarioRepository.ObterPorEmailAsync(input.Email) : _usuarioRepository.ObterPorCpfAsync(input.Cpf));

            if (usuarioAlreadyExists != null)
            {
                return null;
            }

            _usuarioRepository.Adicionar(usuario);

            return usuario;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            return usuario;
        }

        public async Task<Usuario> ObterPorCpfAsync(string cpf)
        {
            var usuario = await _usuarioRepository.ObterPorCpfAsync(cpf);

            return usuario;
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

            return usuario;
        }
    }
}
