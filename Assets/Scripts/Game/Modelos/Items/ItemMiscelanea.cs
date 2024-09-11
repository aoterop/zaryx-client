public class ItemMiscelanea : Item
{
    public byte NivelRequerido { get; set; }

    public ItemMiscelanea(byte nivelRequerido) : base()
    {
        NivelRequerido = nivelRequerido;
    }

    public ItemMiscelanea() : base() { }

    public override byte Tipo() { return (byte)Tipos.Items.MISCELANEA; }
}