namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_PersonajeEscogido : IMensajeSaliente
    {
        public long IdPersonaje { get; set; }
        public byte Clase { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_PERSONAJE_ESCOGIDO; }
        public MS_PersonajeEscogido(long idPersonaje, byte clase)
        {
            IdPersonaje = idPersonaje;
            Clase = clase;
        }
    }
}