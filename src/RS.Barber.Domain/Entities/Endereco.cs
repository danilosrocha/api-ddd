namespace RS.Barber.Domain.Entities
{
    public class Endereco : Entity
    {
        public string Rua { get; set; }
        public string Complemento { get; set; }
        public Guid ClienteId { get; set; }

        // Prop de Navegação do EF
        public virtual Cliente Cliente { get; set; }


        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}

