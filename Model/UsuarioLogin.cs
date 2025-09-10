namespace APICsharp.Models
{
    public class UsuarioLogin
    {
        public string? Email { get; set; }
        public string? NomeUsuario { get; set; }
        public required string Senha { get; set; }
    }
}