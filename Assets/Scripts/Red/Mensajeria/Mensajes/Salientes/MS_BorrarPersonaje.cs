namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_BorrarPersonaje : IMensajeSaliente
    {
        public long IdPersonaje { get; set; }
        public byte Clase { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_BORRAR_PERSONAJE; }

        public MS_BorrarPersonaje(long idPersonaje, byte clase)
        {
            IdPersonaje = idPersonaje;
            Clase = clase;
        }
    }
}