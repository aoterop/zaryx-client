namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_CompraItemTienda : IMensajeSaliente
    {
        public short ItemOfertado { get; set; }
        public short Cantidad { get; set; }

        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_COMPRA_ITEM_TIENDA; }

        public MS_CompraItemTienda(short itemOfertado, short cantidad)
        {
            ItemOfertado = itemOfertado;
            Cantidad = cantidad;
        }
    }
}