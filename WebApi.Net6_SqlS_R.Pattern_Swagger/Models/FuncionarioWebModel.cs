using System.ComponentModel.DataAnnotations;
using WebApi.Net6_SqlS_R.Pattern_Swagger.Enums;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Models
{
    public class FuncionarioWebModel
    {
        [Key] // aqui estamos dizendo ao codigo que nosso Id é uma chave primaria
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DepartamentoEnum Departamento { get; set; }
        public bool Ativo { get; set; }
        public TurnoEnum Turno { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime(); // ja vamos estar instanciando com datetime.Now.ToLocalTime() para para ricar no formato correto.
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime(); // ja vamos estar instanciando com datetime.Now.ToLocalTime() para para ricar no formato correto.

    }
}
