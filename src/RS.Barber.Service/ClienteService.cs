using AutoMapper;
using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Entities;
using RS.Barber.Service.Erros;

namespace RS.Barber.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ClienteErrosService _clienteErros;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ClienteErrosService clienteErros)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteErros = clienteErros;
        }


        public async Task<Cliente> AdicionarClienteAsync(ClienteInput objeto)
        {
            var cliente = _mapper.Map<Cliente>(objeto);

            if (!cliente.EhValido())
            {
                await _clienteErros.TratarErroValidacaoAsync(cliente.ValidationResult);
            }

            var clienteExists = await _clienteRepository.ObterPorCpfAsync(cliente.CPF);

            if (clienteExists != null)
            {
                await _clienteErros.TratarErroComMensagemAsync("Cpf já cadastrado");
            }

            _clienteRepository.Adicionar(cliente);

            return cliente;
        }
    }
}
