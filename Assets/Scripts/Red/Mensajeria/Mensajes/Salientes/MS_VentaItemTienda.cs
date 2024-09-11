namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_VentaItemTienda :IMensajeSaliente
    {
        public int IdTienda { get; set; }
        public short IdItemAVender { get; set; }
        public byte Ranura { get; set; }
        public short Cantidad { get; set; }

        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_VENTA_ITEM_TIENDA; }

        public MS_VentaItemTienda(int idTienda, short idItemAVender, byte ranura, short cantidad)
        {
            IdTienda = idTienda;
            IdItemAVender = idItemAVender;
            Ranura = ranura;
            Cantidad = cantidad;
        }
    }
}