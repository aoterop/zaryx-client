namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_IntercambioSlot : IMensajeSaliente
    {
        public byte SeccionInventario { get; set; }
        public byte RanuraOrigen { get; set; }
        public byte RanuraDestino { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_INTERCAMBIO_SLOTS; }
        public MS_IntercambioSlot(byte seccionInventario, byte ranuraOrigen, byte ranuraDestino) 
        {
            SeccionInventario = seccionInventario;
            RanuraOrigen = ranuraOrigen;
            RanuraDestino = ranuraDestino;
        }
    }
}