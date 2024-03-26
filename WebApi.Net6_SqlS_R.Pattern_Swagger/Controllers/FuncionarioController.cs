using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Models;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Service.FuncionarioService;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        //criando uma injeção de dependencia para o nosso IFuncionarioInterface para que possamos usar os metodos que estão la 

        private readonly IFuncionarioInterface _funcionarioInterface; // este elemento que criamos aqui dentro da classa vai receber o que vem no nosso contrutor funcionarioInterface que é o nosso IFuncionarioInterface em Service/FuncionarioService/FuncionarioService.cs
        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
                _funcionarioInterface = funcionarioInterface;
        }
        //Atenção: quando eu fizer uma injeção de dependencia da interface FuncionarioService e IfuncionarioInterface eu quero que seja utilizado os metodos que estão na FuncionarioService, ou seja vá até o Program.cs e crie um Builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>(); para que possamos utilizar os metodos que estão na FuncionarioService

        [HttpGet]   
        public async Task <ActionResult<ServiceResponse<List<FuncionarioWebModel>>>> GetFuncionario() //aqui vamos retornar um ActionResult do tipo Task <ActionResult>, ou seja, vamos retornar uma lista de funcionariosModels dentro do serviceResponse, crie um elemento do tipo ServiceResponse<List<FuncionarioWebModel>> quando dermos ok ele vai mandar um status code 200, se der erro ele vai mandar um status code 404
        {
            return Ok( await _funcionarioInterface.GetFuncionario());
        }
        //Nosso controller está totalmente enchuto,  dando apenas um retorno nada mais que isso
        //o controller não tem que ter regra de negocio, ele apenas chama o serviço e retorna o que o serviço retornar
        [HttpGet("{id}")]
        public async Task <ActionResult<ServiceResponse<FuncionarioWebModel>>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioWebModel> serviceResponse = await _funcionarioInterface.GetFuncionarioById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task <ActionResult<ServiceResponse<List<FuncionarioWebModel>>>> AddFuncionario(FuncionarioWebModel novoFuncionario)
        {
            return Ok(await _funcionarioInterface.AddFuncionario(novoFuncionario));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioWebModel>>>> UpdateFuncionario(FuncionarioWebModel funcionarioAtualizado)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = await _funcionarioInterface.UpdateFuncionario(funcionarioAtualizado);
            return Ok(serviceResponse);
        }


        [HttpPut("inativaFuncionario")] //como esta rota é so o nome e não é um parametro não precisamos por entre chaves
        public async Task <ActionResult<ServiceResponse<List<FuncionarioWebModel>>>> InativaFuncionario(int Id)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = await _funcionarioInterface.InativaFuncionario(Id);
            return Ok(serviceResponse);
        }
    }
}
