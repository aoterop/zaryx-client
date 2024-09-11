using Newtonsoft.Json;

public static class Deserializador
{
    public static T Deserializar<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}