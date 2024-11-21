using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BibliotecKure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BibliotecKure.Contexts
{
    //Classe que tetá as informações do banco de dados
    public class Context : DbContext
    {
        // Criar um método contrutor 
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        //OnConfiguring -> possui a configuração da conexão com o banco de dados

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            // colocar as informações do banco

            // As configurações existem ?
            if (!optionsBuilder.IsConfigured)
            {
                //A string de conexao do banco de dado

                // data source => nome do servidor do banco de dados
                // Initial Catalog => nome do banco de dados
                // User id e password => informações de acesso ao servidor do banco de dados
                //ALUNOS:

                //optionsBuilder.UseSqlServer(@"Data Source=NOTE13-S28\\SQLEXPRESS;
                //Initial Catalog = Bibliotec_MVC;
                //User Id=sa; 
                //Password=123;
                //Integrated Security=true; 
                //TrustServerCertificate = true");

                optionsBuilder.UseSqlServer(@"Data Source=NOTE13-S28\\SQLEXPRESS; 
                Initial Catalog = Bibliotec_MVC; 
                User Id=sa; 
                Password=123; 
                Integrated Security=true; 
                TrustServerCertificate = true");
            }
        }
        // As referencias das nossas tabelas no banco de dados:
        public DbSet<Categoria> Categoria { get; set; }
        //Curso
        public DbSet<Curso> Curso { get; set; }
        //Livro
        public DbSet<Livro> Livro { get; set; }
        //Usuario
        public DbSet<Usuario> Usuario { get; set; }
        //LivroCategoria
        public DbSet<LivroCategoria> LivroCategoria { get; set; }
        //LivroReserva
        public DbSet<LivroReserva> LivroReserva { get; set; }
    }
}