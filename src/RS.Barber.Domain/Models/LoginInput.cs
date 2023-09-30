namespace RS.Barber.Domain.Models
{
    public class LoginInput
    {
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
    }
}
