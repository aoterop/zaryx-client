public class MS_CrearPersonaje : IMensajeSaliente
{
    public string Nombre { get; set; }
    public byte Clase { get; set; }
    public byte Peinado { get; set; }
    public byte AspectoFacial { get; set; }

    public MS_CrearPersonaje(string nombre, byte clase, byte peinado, byte aspectoFacial)
    {
        Nombre = nombre;
        Clase = clase;
        Peinado = peinado;
        AspectoFacial = aspectoFacial;
    }

    public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_CREAR_PERSONAJE; }
}