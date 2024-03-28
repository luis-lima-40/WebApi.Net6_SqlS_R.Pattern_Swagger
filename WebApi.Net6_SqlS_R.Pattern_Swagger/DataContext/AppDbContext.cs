using Microsoft.EntityFrameworkCore;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Models;


namespace WebApi.Net6_SqlS_R.Pattern_Swagger.DataContext
{
    public class AppDbContext:DbContext // faça uma herança de DbContext que é uma herença do EntityFrameworkCore   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) //DbContextOptions<>, dentro dele colo o nome do seu DbContext que nesse caso é o "AppDbContext" DbContextOptions<AppDbContext> que vai se chamar options e fora do parenteses coloque base(options) ficando assim public AppDbContext(DbContextOptions<AppDbContext> options): base(options)   Decorre esta linha de codigo pois irá utilizar ela sempre que for fazer uma conexão com o banco.. //ctor tab tab para criar um construtor
        {
                // o corpo sera vazio
        }

        public DbSet<FuncionarioWebModel> Funcionarios { get; set; } // DbSet<> é uma coleção de entidades que representa uma tabela no banco de dados, dentro do <> coloque o nome da sua classe que representa a tabela, nesse caso é a classe FuncionarioModel, fora do <> coloque o nome da propriedade que vai representar a tabela no banco de dados, nesse caso é Funcionarios, ficando assim public DbSet<Models.FuncionarioModel> Funcionarios { get; set; }

        //va em appsettings.json e coloque a string de conexão com o banco de dados

    }
}
