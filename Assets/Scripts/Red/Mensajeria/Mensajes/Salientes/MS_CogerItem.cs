namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_CogerItem : IMensajeSaliente
    {
        public long IdItemSuelo { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_COGER_ITEM; }

        public MS_CogerItem(long idItemSuelo)
        {
            IdItemSuelo = idItemSuelo;
        }
    }
}