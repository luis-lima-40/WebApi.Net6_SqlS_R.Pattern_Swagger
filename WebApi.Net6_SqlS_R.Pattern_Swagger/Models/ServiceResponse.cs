using System.Security.Principal;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Models
{
    public class ServiceResponse<T>  //Vamos incluir <T> para receber dados genericos, ou seja, se formos estender esse projeto e criar outras classes diferentes de funcionario, podemos reutilizar essa mesma classe Servise Response para as demais classes que surgirem, tipo ProdutoModel, SetoresModel mesmo com dados diferentes podemos utilizar esse mesmo service response, que irá mostras os dados sejam quais fores, uma mensagem de sucesso ou não  e um retorno de sucesso ou nao
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty; //ja vamos instanciar essa propriedade Mensagem como sendo uma string vazia
        public bool Sucesso { get; set; } = true; // Ja setamos essa propriedade como sendo verdadeira desde o inicio pois ela representa se ouve a comunicação da API com sucesso

    }
}
