using WebApi.Net6_SqlS_R.Pattern_Swagger.Enums;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Models;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Service.FuncionarioService
{
    public interface IFuncionarioInterface
    {
        //Vamos usar task para que possamos usar o async e o await, são metodos assincronos, ServiceResponse é nossa model que mostra os dados da nossa requisição que contes os dados da funcionarioModel, uma mensagem e um booleano de true ou false, sucesso ou não, dentro do noss serviceresponse vamos retornar uma lista ou um unico funcionario, ou seja, um objeto, por isso usamos o <List<FuncionarioModel>> ou <FuncionarioModel>
        Task<ServiceResponse<List<FuncionarioWebModel>>> GetFuncionario();
        Task<ServiceResponse<FuncionarioWebModel>> GetFuncionarioById(int id);
        Task<ServiceResponse<List<FuncionarioWebModel>>> AddFuncionario(FuncionarioWebModel novoFuncionario);
        Task<ServiceResponse<List<FuncionarioWebModel>>> UpdateFuncionario(FuncionarioWebModel funcionarioAtualizado);
        Task<ServiceResponse<List<FuncionarioWebModel>>> DeleteFuncionario(int id);
        Task<ServiceResponse<List<FuncionarioWebModel>>> InativaFuncionario(int id);
        Task<ServiceResponse<List<FuncionarioWebModel>>> AtivaFuncionario(int id);  
        Task<ServiceResponse<List<FuncionarioWebModel>>> GetFuncionarioByDepartamento(DepartamentoEnum Departamento);
        
    }
}
