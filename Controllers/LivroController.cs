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
            novoLivro.Descricao = form["Descricao"].ToString();
            novoLivro.Editora = form["Editora"].ToString();
            novoLivro.Escritor = form["Escritor"].ToString();
            novoLivro.Idioma = form["Idioma"].ToString();
            //trabalhar com imagens
            // a parte de colocar imagem == 0

            if(form.Files.Count > 0){
                //Promeiro passo:
                    //Amazenaremos o arquivo enviado pelo usuário  
                    var arquivo = form.Files[0];

                //segundo passo:
                    var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Livros" );
                    //Validarmos se a pasta que será armazenada as imagens, existe. Caso não exista, criaremos uma nova pasta
                    if(Directory.Exists(pasta)){
                        //Criar a pasta:
                        Directory.CreateDirectory(pasta);
                    }

                //Terceiro passo:
                //Criar a variavel para armazenar o caminho em que meu arquivo estara, além do nome dele
                var caminho = Path.Combine(pasta, arquivo.FileName);

                using (var stream = new FileStream(caminho, FileMode.Create)){
                // Copiou o arquivo para o diretório 
                arquivo.CopyTo(stream);                  
                    
                };

                novoLivro.Imagem = arquivo.FileName;
            }else{
                novoLivro.Imagem = "podrao.png";
            }


            context.Livro.Add(novoLivro);
            context.SaveChanges();

            //SEGUNDA PARTE: É adicionar dentro de livroCategoria a categoria que pertence ao novoLivro
            //Lista a tabela livroCategoria

            List<LivroCategoria> ListaLivroCategorias = new List<LivroCategoria>(); //Lista as categorias

            //Array que possui as categorias selecionadas pelo usuário

            string [] categoriasSelecionadas = form ["Categoria"].ToString().Split(','); 
            //Ação, terror, suspense 

            foreach(string categoria in categoriasSelecionadas){
                LivroCategoria livroCategoria = new LivroCategoria();
                
                livroCategoria.CategoriaID = int.Parse(categoria);
                livroCategoria.LivroID = novoLivro.LivroID;
                //Adicionamos o obj livroCategoria dejtro da lista ListaLivroCategorias
                ListaLivroCategorias.Add(livroCategoria);
            }
            //Peguei a coleção da listaLivroCategorias e coloquei na tabela LivroCategoria
            context.LivroCategoria.AddRange(ListaLivroCategorias);

            context.SaveChanges();

            return LocalRedirect("~/Livro/Cadastro");
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}   