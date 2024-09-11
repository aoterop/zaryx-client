namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_ArrojarItem : IMensajeSaliente
    {
        public byte SeccionInventario { get; set; }
        public byte Ranura { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_ARROJAR_ITEM; }

        public MS_ArrojarItem(byte seccionInventario, byte ranura)
        {
            SeccionInventario = seccionInventario;
            Ranura = ranura;
        }
    }
}