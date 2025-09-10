using APICsharp.Data;
using APICsharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace APICsharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ConnectionContext _context;
        public LoginController(ConnectionContext context)
        {
            _context = context;
        }
        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] Usuario user)
        {
            try
            {
                var hasher = new PasswordHasher<Usuario>();
                user.Senha = hasher.HashPassword(user, user.Senha);
                _context.Usuarios.Add(user);
                _context.SaveChanges();
                return Ok(new { Mensagem = "Usuario registrado com sucesso!" });
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new { Erro = "Falha ao registrar usuario: ", ex });
            }
        }

        [HttpPost("autenticar")]
        public IActionResult AutenticarUsuario([FromBody] UsuarioLogin user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.NomeUsuario))
                {
                    return BadRequest(new { Mensagem = "Informe Email ou NomeUsuario para login." });
                }
                var encontrado = _context.Usuarios
                    .FirstOrDefault(u =>
                        (user.NomeUsuario != null && u.NomeUsuario == user.NomeUsuario) ||
                        (user.Email != null && u.Email == user.Email));
                if (encontrado == null)
                {
                    return Unauthorized(new { Mensagem = "Usuario ou Senha invalidos." });
                }

                var hasher = new PasswordHasher<Usuario>();

                var resultado = hasher.VerifyHashedPassword(encontrado, encontrado.Senha, user.Senha);

                if (resultado == PasswordVerificationResult.Success)
                {
                    return Ok(new { Mensagem = "Login realizado com sucesso!" });
                }
                else
                {
                    return Unauthorized(new { Mensagem = "Usuario ou Senha invalidos." });
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro ao tentar fazer login: ", ex });
            }
        }
        [HttpGet("visualizar/{id}")]
        public IActionResult VisualizarInformacoes([FromRoute] int id)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(new { Mensagem = "Nenhum Usuario encontrado" });
                }
                else
                {
                    return Ok(new { user.Email, user.NomeUsuario });
                }
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new { Error = "Ocorreu um erro ao resgatar o usuario: ", ex });
            }

        }

    }
}