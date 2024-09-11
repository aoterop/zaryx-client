public class MaestriaGuerrero : Item
{
    public byte NivelMinimo { get; set; }
    public byte NumeroMaestria { get; set; }

    public MaestriaGuerrero(byte nivelMinimo, byte numeroMaestria) : base()
    {
        NivelMinimo = nivelMinimo;
        NumeroMaestria = numeroMaestria;
    }

    public MaestriaGuerrero() : base() { }
    public override byte Tipo() { return (byte)Tipos.Items.MAESTRIA_GUERRERO; }

}