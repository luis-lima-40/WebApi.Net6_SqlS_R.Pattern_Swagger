using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Runtime.Intrinsics.X86;
//using System.Text.RegularExpressions;
using WebApi.Net6_SqlS_R.Pattern_Swagger.DataContext;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Enums;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Models;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface // o funcionario service implementa a interface IFuncionarioInterface, ou seja aqui estamos criando um contrato, dentro da IFuncionarioInterface vai conter todos os metodos que precisam ser implementados.
    {
        //aqui no funcionarioServices vamos colocar nosso applicationDbContext para que possamos fazer as operações no banco de dados
        //vamos fazer uma injeção de dependencia, ou seja, vamos passar o nosso AppDbContext como parametro no construtor   
        //vamos criar um elemento privado apenas para leitura que é do tipo appDbContext que vamos chamar de _context quando pegar no nosso contrutor abaixo o context que é o parametro que estamos passando, vamos atribuir a ele o _context que é o elemento privado que criamos
        //quando usarmos no nosso codigo o _context ele vai estar se referindo ao context que é o nosso AppDbContext e vamos ter acesso a todos os nossos bancos de dados e tabelas
        private readonly AppDbContext _context; 
        public FuncionarioService(AppDbContext context)
        {
            _context = context;    
        }
        public async Task<ServiceResponse<List<FuncionarioWebModel>>> AddFuncionario(FuncionarioWebModel novofuncionario)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //aqui é para mostrar nossa lista de clientes que é o que vamos retornar
            try
            {
                if (novofuncionario == null)
                {   
                    serviceResponse.Dados = null; //aqui estamos dizendo que o serviceResponse.Dados vai receber null
                    serviceResponse.Mensagem = "Favor informar os dados para cadastro!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                _context.Add(novofuncionario); //aqui estamos dizendo que vamos adicionar um novo funcionario no banco de dados
                await _context.SaveChangesAsync(); //aqui estamos dizendo que vamos salvar as alterações no banco de dados
                serviceResponse.Dados = _context.Funcionarios.ToList(); //aqui estamos dizendo que o serviceResponse.Dados vai receber todos os funcionarios que estão no banco de dados, para isso usamos o await para esperar a resposta do banco de dados, e usamos o ToListAsync() para pegar todos os dados do banco de dados
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> AtivaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //instancie o serviceResponse
            try
            {
                FuncionarioWebModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);
                
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum funcionário funcionario selecionado para ativar!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }


                funcionario.Ativo = true;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //instancie o serviceResponse
            try
            {
                FuncionarioWebModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id); //aqui estamos criando uma variavel chamada funcionario do tipo FuncionarioModel que vai receber o primeiro funcionario que tiver o id igual ao id que estamos passando como parametro
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum funcionário selecionado para deleção!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> GetFuncionario() // coloque sempre o async se vc criou medotos do tipo Task na sua IfuncionarioInterface, aqui vamos retornar uma lista de funcionariosModels dentro do serviceResponse, crie um elemento do tipo ServiceResponse<List<FuncionarioWebModel>>
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //instancie o serviceResponse
            try
            {
                serviceResponse.Dados =  _context.Funcionarios.ToList(); //aqui estamos dizendo que o serviceResponse.Dados vai receber todos os funcionarios que estão no banco de dados, para isso usamos o await para esperar a resposta do banco de dados, e usamos o ToListAsync() para pegar todos os dados do banco de dados
                //serviceResponse.Dados = await _context.Funcionarios.ToListAsync(); //aqui estamos dizendo que o serviceResponse.Dados vai receber todos os funcionarios que estão no banco de dados, para isso usamos o await para esperar a resposta do banco de dados, e usamos o ToListAsync() para pegar todos os dados do banco de dados
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenum dado encontrado no banco de dados!";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> GetFuncionarioByDepartamento(DepartamentoEnum Departamento)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //instancie o serviceResponse
            try
            {
                serviceResponse.Dados = _context.Funcionarios.Where(x => x.Departamento == Departamento).ToList(); //aqui estamos dizendo que o serviceResponse.Dados vai receber todos os funcionarios que estão no banco de dados, para isso usamos o await para esperar a resposta do banco de dados, e usamos o ToListAsync() para pegar todos os dados do banco de dados
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenum dado encontrado no banco de dados!";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
            
        }

        public async Task<ServiceResponse<FuncionarioWebModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioWebModel> serviceResponse = new ServiceResponse<FuncionarioWebModel>(); //instancie o serviceResponse
            try
            {
                FuncionarioWebModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id); //aqui estamos criando uma variavel chamada funcionario do tipo FuncionarioModel que vai receber o primeiro funcionario que tiver o id igual ao id que estamos passando como parametro
                serviceResponse.Dados = funcionario;
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum dado encontrado correspondente ao Id informado!";
                    serviceResponse.Sucesso = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> InativaFuncionario(int id)
        {
         ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>>(); //instancie o serviceResponse
            try
            {
                FuncionarioWebModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id); //aqui estamos criando uma variavel chamada funcionario do tipo FuncionarioModel que vai receber o primeiro funcionario que tiver o id igual ao id que estamos passando como parametro
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum funcionário encontrado correspondente ao Id informado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                funcionario.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse 
        }

        public async Task<ServiceResponse<List<FuncionarioWebModel>>> UpdateFuncionario(FuncionarioWebModel funcionarioAtualizado)
        {
            ServiceResponse<List<FuncionarioWebModel>> serviceResponse = new ServiceResponse<List<FuncionarioWebModel>> (); //instancie o serviceResponse   
            try
            {
                FuncionarioWebModel funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == funcionarioAtualizado.Id); //aqui estamos criando uma variavel chamada funcionario do tipo FuncionarioModel que vai receber o primeiro funcionario que tiver o id igual ao id que estamos passando como parametro // AsNoTracking(). serve para não dar erro no banco de dados na hora de atualizar

                //AsNoTracking() Quando você utiliza o Entity Framework e precisa fazer um select específico e logo depois fazer um update ou atualizar a informação do database, normalmente vai voltar um erro ao tentar atualizar os dados, isso porque estou tentando selecionar e com o mesmo objeto da mesma model tentando atualizar.
                //Então é agora que entra o AsNoTracking.Para utilizar esse comando, é necessário importar o using Microsoft.EntityFrameworkCore.Depois de importar, é necessário colocar o comando dentro do método de select.
                // Usando o AsNoTracking
                //_db.UserModels.AsNoTracking().FirstOrDefault(x => x.UserId == id);
                //O código 1.3 mostra que depois do objeto basta colocar o AsNoTracking() antes da condição.
                //Em resumo, depois de colocar esse comando antes da condição o update não mais deu problema, ou seja, a atualização dos dados aconteceu perfeitamente.

                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum funcionário foi Sellecionado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(funcionarioAtualizado);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message; //se der algum erro, a mensagem vai receber a mensagem de erro
                serviceResponse.Sucesso = false; //e a propriedade sucesso vai receber false
            }
            return serviceResponse; //retorne o serviceResponse
        }
    }
}
