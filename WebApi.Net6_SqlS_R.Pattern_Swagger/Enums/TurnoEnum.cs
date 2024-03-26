using System.Text.Json.Serialization;

namespace WebApi.Net6_SqlS_R.Pattern_Swagger.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    //para que vc transforme um enum em uma string, uma linha antes do public enum  use [JsonConverter(typeof(JsonStringEnumConverter))], faça a importação do using System.Text.Json.Serialization;
    public enum TurnoEnum
    {
        Manha,
        Tarde,
        Noite,
        Integral
    }
}
