public class Item
{
    public short IdItem { get; set; }
    public string NombreItem { get; set; }
    public string DetallesItem { get; set; }
    public long Precio { get; set; }
    public bool EsArrojable { get; set; }

    public virtual byte Tipo() { return (byte)Tipos.Items.NO_ESPECIFICADO; }

    public Item(short idItem, string nombreItem, string detallesItem, long precio, bool esArrojable)
    {
        IdItem = idItem;
        NombreItem = nombreItem;
        DetallesItem = detallesItem;
        Precio = precio;
        EsArrojable = esArrojable;
    }

    public Item() { }
}