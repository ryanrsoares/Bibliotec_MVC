using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec_mvc.Controllers
{
    [Route("[controller]")]
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }
        Context context = new Context();
        public IActionResult Index()
        {
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            List <Livro> listaLivros = context.Livro.ToList();
            //verificar se o livro tem reserva ou não
            var livrosReservados = context.LivroReserva.ToDictionary(livro => livro.LivroID, livror => livror.DtReserva);

            ViewBag.Livros = listaLivros;
            ViewBag.livrosComReserva = livrosReservados;
            


            return View();
        }

        [Route("Cadastro")]
        //método que retorna a tela de cadastro:
        public IActionResult Cadastro(){

            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            ViewBag.Categoria = context.Categoria.ToList();
            //Retorna a View de cadastro:
            return View();
        }

        //Método para cadastrar um livro:
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form){

            Livro novoLivro = new Livro();

            //O que meu usuário escrever no formulário será atribuido ao livro novo
            
            novoLivro.Nome = form["Nome"].ToString();

            //Descrição
            novoLivro.Descricao = form["Descricao"].ToString();
            //Editora
            novoLivro.Editora = form["Editora"].ToString();
            //Escritor
            novoLivro.Escritor = form["Escritor"].ToString();
            //Idioma
            novoLivro.Idioma = form["Idioma"].ToString();

            //img
            context.Livro.Add(novoLivro);

            context.SaveChanges();
        
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}