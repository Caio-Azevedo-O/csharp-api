namespace APICsharp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string NomeUsuario { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
