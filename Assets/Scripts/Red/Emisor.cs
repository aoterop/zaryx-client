public static class Emisor
{
    public static void Enviar(byte tipoMensaje, string mensaje)
    {
        GestorDeRed.Red.EnviarMensaje(tipoMensaje.ToString() + "§" + mensaje);
    }
}