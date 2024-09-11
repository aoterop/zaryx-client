public class ItemEquipo : Item
{
    public byte NivelRequerido { get; set; }
    public byte ClasePermitida { get; set; }

    public ItemEquipo(byte nivelRequerido, byte clasePermitida) : base()
    {
        NivelRequerido = nivelRequerido;
        ClasePermitida = clasePermitida;
    }

    public ItemEquipo() : base() { }
}