namespace RS.Barber.Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (!endereco.EhValido())
            {
                ValidationResult = endereco.ValidationResult;
                return;
            }

            Enderecos.Add(endereco);
        }

        public void DefinirComoAtivo()
        {
            Ativo = true;
            Excluido = false;
        }

        public void DefinirComoExcluido()
        {
            Ativo = false;
            Excluido = true;
        }


        public override bool EhValido()
        {
            if (string.IsNullOrEmpty(Nome)) AdicionarErroValidacao(nameof(Nome), "O campo nome está vazio!");

            return ValidationResult.Count == 0;
        }
    }
}
