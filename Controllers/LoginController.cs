using Bibliotec.Contexts;
using BibliotecKure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecKure.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();


        public IActionResult Index()
        {
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            string emailInformado = form["Email"];
            string senhaInformada = form["Senha"];


            Usuario usuarioBuscado = context.Usuario.FirstOrDefault(usuario => usuario.Email == emailInformado && usuario.Senha == senhaInformada)!;

            if(usuarioBuscado == null)
            {
                Console.WriteLine($"Dados inválidos");
                return LocalRedirect("~/Login");
            }else{
                Console.WriteLine($"Eba, você entrou!");
                return LocalRedirect("~/Livro");
                
            }

            // List<Usuario>listaUsuario = context.Usuario.ToList();

            // foreach(Usuario usuario_ in listaUsuario)
            // {
            //     if(usuario_.Email == emailInformado && usuario_.Senha == senhaInformada){
            //         // Usuario logado
            //     }else{
            //         // Usuario não encontrado
            //     }
            // }









            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}