public class ItemEquipoDefensivo : ItemEquipo
{
    public byte TipoEquipoDefensivo { get; set; }
    public short DefensaItem { get; set; }
    public byte VelocidadExtra { get; set; }

    public ItemEquipoDefensivo(byte tipoEquipoDefensivo, short defensaItem, byte velocidadExtra) : base()
    {
        TipoEquipoDefensivo = tipoEquipoDefensivo;
        DefensaItem = defensaItem;
        VelocidadExtra = velocidadExtra;
    }

    public ItemEquipoDefensivo() : base() { }

    public override byte Tipo() { return (byte)Tipos.Items.EQUIPO_DEFENSIVO; }

}