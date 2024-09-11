namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_SolicitarPersonajes :IMensajeSaliente
    {
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_SOLICITAR_PERSONAJES; }
    }
}