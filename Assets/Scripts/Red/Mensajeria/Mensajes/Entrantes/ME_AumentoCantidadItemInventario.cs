namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_AumentoCantidadItemInventario
    {
        public short CantidadAumentadad { get; set; }
        public byte SeccionInventario { get; set; }
        public byte Ranura { get; set; }

        public ME_AumentoCantidadItemInventario() { }
    }
}