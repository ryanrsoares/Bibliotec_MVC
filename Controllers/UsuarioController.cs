using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec_mvc.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        // Criando um obj da classe context
        Context context = new Context();

        //O método esta retornando a view Usuario/Indesx.cshtml
        public IActionResult Index()
        {
            //pegar as informações da session que sao necessarios para que aparece as informações do usuario

            int id = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;


            //busquei o usuário que está logado (Beatriz)
            Usuario usuarioEncontrado = context.Usuario.FirstOrDefault(usuario => usuario.UsuarioID == id)!;
            //se n for encontrado ninguem
            if (usuarioEncontrado == null){
                return NotFound();
            }

            //Procurar o curso que meu usuário está cadastrado
            Curso cursoEcontrado = context.Curso.FirstOrDefault(curso => curso.CursoID == usuarioEncontrado.CursoID )!;

            if (cursoEcontrado == null){
                //Preciso que vc mande essa mensagem para a view
                ViewBag.Curso = "O usuário não possui curso cadastrado";
            }else{
                //Preciso que vc mande p nome do curos para a view
                ViewBag.Curso = cursoEcontrado.Nome;
            }

            ViewBag.Nome = usuarioEncontrado.Nome;
            ViewBag.Email = usuarioEncontrado.Email;
            ViewBag.Contato = usuarioEncontrado.Contato;
            ViewBag.DtNasc = usuarioEncontrado.DtNascimento.ToString("dd/MM/yyyy");
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}