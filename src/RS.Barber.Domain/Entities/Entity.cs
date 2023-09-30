using System.ComponentModel.DataAnnotations.Schema;

namespace RS.Barber.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new Dictionary<string, string>();
        }

        public Guid Id { get; set; }

        [NotMapped]
        public IDictionary<string, string> ValidationResult { get; set; }

        public void AdicionarErroValidacao(string erro, string mensagem)
        {
            ValidationResult.Add(erro, mensagem);
        }

        public abstract bool EhValido();
    }
}
