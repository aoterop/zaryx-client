public class ItemPersonaje
{
    public short ReferenciaItem { get; set; }
    public short Cantidad { get; set; }
    public byte NivelItem { get; set; }
    public long ExperienciaItem { get; set; }
    public byte RanuraInventario { get; set; }

    public ItemPersonaje(short referenciaItem, short cantidad, byte nivelItem, long experienciaItem, byte ranuraInventario)
    {
        ReferenciaItem = referenciaItem;
        Cantidad = cantidad;
        NivelItem = nivelItem;
        ExperienciaItem = experienciaItem;
        RanuraInventario = ranuraInventario;
    }

    public ItemPersonaje Clone()
    {
        return new ItemPersonaje
        {
            ReferenciaItem = this.ReferenciaItem,
            Cantidad = this.Cantidad,
            NivelItem = this.NivelItem,
            ExperienciaItem= this.ExperienciaItem,
            RanuraInventario = this.RanuraInventario
        };
    }

    public ItemPersonaje() { }
}