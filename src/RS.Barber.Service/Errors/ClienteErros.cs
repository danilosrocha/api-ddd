using System.Text.Json;

namespace RS.Barber.Service.Erros
{
    public interface IClienteErrosService
    {
        Task TratarErroValidacaoAsync(IDictionary<string, string> validationResult);
        Task TratarErroComMensagemAsync(string mensagem);
    }

    public class ClienteErrosService : IClienteErrosService
    {
        private async Task LancarErroAsync(object errorResponse)
        {
            throw new Exception(JsonSerializer.Serialize(errorResponse));
        }

        private IEnumerable<object> ObterErrosDominio(IDictionary<string, string> validationResult)
        {
            var erros = new List<string>();
            foreach (var erro in validationResult)
            {
                erros.Add(erro.Value);
            }
            return erros;
        }

        public async Task TratarErroValidacaoAsync(IDictionary<string, string> validationResult)
        {
            var errorResponse = new { Erros = ObterErrosDominio(validationResult) };
            await LancarErroAsync(errorResponse);
        }

        public async Task TratarErroComMensagemAsync(string mensagem)
        {
            var errorResponse = new { Error = mensagem };
            await LancarErroAsync(errorResponse);
        }
    }
}
