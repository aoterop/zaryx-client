public class MaestriaTirador : Item
{
    public byte NivelMinimo { get; set; }
    public byte NumeroMaestria { get; set; }

    public MaestriaTirador(byte nivelMinimo, byte numeroMaestria) : base()
    {
        NivelMinimo = nivelMinimo;
        NumeroMaestria = numeroMaestria;
    }

    public MaestriaTirador() : base() { }

    public override byte Tipo() { return (byte)Tipos.Items.MAESTRIA_TIRADOR; }
}