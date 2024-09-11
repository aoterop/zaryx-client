namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_NuevoItemInventario
    {
        public short Cantidad { get; set; }
        public short IdItem { get; set; }
        public byte SeccionInventario { get; set; }
        public byte Ranura { get; set; }

        public ME_NuevoItemInventario() { }
    }
}