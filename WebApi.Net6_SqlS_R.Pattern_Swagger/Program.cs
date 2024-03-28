using Microsoft.EntityFrameworkCore;
using WebApi.Net6_SqlS_R.Pattern_Swagger.DataContext;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Service.FuncionarioService;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
           policy =>
              {
                  policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                  });
                    
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>(); //vamos registrar a nossa interface e o nosso servi�o, ou seja, vamos dizer para o nosso projeto que toda vez que precisarmos de um IFuncionarioInterface, ele vai instanciar um FuncionarioService    

builder.Services.AddDbContext<AppDbContext>(options => //vamos configurar o EntityFramework para usar o SQLServer
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);
app.Run();



//LEIA-ME-Program.cs.TXT
//
//
//24-03-22 Vamos criar este projeto usando boas praticas
//vamor colocar toda nossa logica nos controllers, vamos criar servi�os, nesses servi�os
//vamos nos comunicar com o banco de dados e o controller basicamente ir�
//consultar o servi�o para mostrar ao usu�rio, ou seja , vamos utilizar  esse
//padr�o chamado de Repository Pattern
//vamos tamb�m utilizar o padr�o CODE FIRST, ou seja, vamos criar as classes para
//posteriormente criar a base de dados
//vamos criar uma pasta chamada Models e dentro dela vamos criar uma classe chamada FuncionarioModel.cs
//vamos criar uma pasta chamada Enums e dentro dela vamos criar duas classes DepartamentoEnum.cs e TurnoEnum.cs
//ServiceResponse.cs vamos criar uma classe para retornar as respostas dos servi�os, quando utilizarmos
//o Angular para fazer o FontEnd, ele vai receber um objeto do tipo ServiceResponse para tratar a mensagem
//o sucesso o erro e os dados que ficar� dentro do serviceResponse, esse serviceResponse ser� nossa primeira camada de comunica��o
//vamos criar no banco de dados apenas o FuncionarioModel.cs, pois n�o precisamos de uma tabela de ServiceResponse no banco..
//Vamos utilizar o EnturyFramework para fazer a comunica��o com o banco de dados, pelo Nuget vamos instalar o pacote do EntityFrameworkCore vers�o 7.0.9
//ainda no Nugget vamos instalar o pacote Microsoft.EntityFrameworkCore.SqlServer vers�o 7.0.9
//ainda no Nugget vamos instalar o pacote Microsoft.EntityFrameworkCore.tools vers�o 7.0.9
//ainda no Nugget vamos instalar o pacote Microsoft.EntityFrameworkCore.designer vers�o 7.0.9
//vamos criar uma pasta chamada DataContext e dentro dela vamos criar uma classe chamada AppDbContext.cs
//
//
//na aba inferior do Visual Studio Code, vamos clicar em Terminal Packege Manager Console  e digitar o comando:
//Add-Migration InitialCreate
//esse comando ir� criar uma pasta chamada Migrations e dentro dela um arquivo chamado 20220324185344_InitialCreate.cs
//caso de algum erro e precise recriar a migra��o, use o comando Remove-Migration
//esse arquivo � uma classe que ir� criar a base de dados, vamos executar o comando:
//Update-Database
//esse comando ir� criar a base de dados, vamos verificar no SQLServer se a base de dados foi criada
//vamos criar uma pasta chamada Controllers e dentro dela vamos criar uma classe chamada FuncionarioController.cs
//
//ao rodar o font end Angular que consome os dados de uma api AspNet 6, quando der o ng serve e iniciar
//a execu��o da pagina, caso n�o carregue verisique no inspecionar da pagina na aba conselo
//
//se refornar o erro:
//Access to XMLHttpRequest at �http://a.domain.com/page-cors-subdomain-a.txt� from origin �http://b.domain.com� has been blocked by CORS policy: 
//No �Access-Control-Allow-Origin� header is present on the requested resource.
// 
//� porque sua aplica��o back end em AspNet est� dando conflito com o nagegador que possui essa camada
//de verifica��o CORS.
//
//para solucionar o problema, no seu BackEnd em apsNet6, no Progran.cs
//
//no topo apos>    var builder = WebApplication.CreateBuilder(args);
//crie o seguinte codigo:
//
//
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//           policy =>
//              {
//                  policy.WithOrigins("http://localhost:4200")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//                  });
//                    
//});
//
//no final da pagina, antes da linha>    app.Run();
//
//crie o seguinte codigo:
//app.UseCors(MyAllowSpecificOrigins);
//
//assim vc resolve esse erro e sua aplica��o fontEnd em angular vai conseguir comdumir todos os metodos da sua api
