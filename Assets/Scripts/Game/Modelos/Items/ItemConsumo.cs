public class ItemConsumo : Item
{
    public short CuraHp { get; set; }
    public short CuraMp { get; set; }

    public ItemConsumo(short curaHp, short curaMp) : base()
    {
        CuraHp = curaHp;
        CuraMp = curaMp;
    }

    public override byte Tipo() { return (byte)Tipos.Items.CONSUMO; }
}