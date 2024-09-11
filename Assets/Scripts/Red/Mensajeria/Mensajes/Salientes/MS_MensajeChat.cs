namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_MensajeChat : IMensajeSaliente
    {
        public byte TipoMensaje { get; set; }
        public string Texto { get; set; }
        public string Receptor { get; set; }

        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_MENSAJE_CHAT; }

        public MS_MensajeChat(byte tipoMensaje, string texto, string receptor) 
        {
            TipoMensaje = tipoMensaje;
            Texto = texto;
            Receptor = receptor;
        }
    }
}