using Newtonsoft.Json;
public static class Serializador
{
    public static string Serializar(object mensaje)
    {
        return JsonConvert.SerializeObject(mensaje);
    }
}
