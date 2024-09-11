public class ItemEquipoOfensivo : ItemEquipo
{
    public short RatioCritico { get; set; }
    public short AtaqueCritico { get; set; }
    public short AtaqueMin { get; set; }
    public short AtaqueMax { get; set; }

    public ItemEquipoOfensivo(short ratioCritico, short ataqueCritico, short ataqueMin, short ataqueMax) : base()
    {
        RatioCritico = ratioCritico;
        AtaqueCritico = ataqueCritico;
        AtaqueMin = ataqueMin;
        AtaqueMax = ataqueMax;
    }

    public ItemEquipoOfensivo() :base() { }

    public override byte Tipo() { return (byte)Tipos.Items.EQUIPO_OFENSIVO; }
}