namespace Assets.Scripts.Personajes
{
    public class Guerrero : IPersonaje
    {
        public long IdPersonaje { get; set; }
        public byte Peinado { get; set; }
        public byte AspectoFacial { get; set; }
        public bool EsAdmin { get; set; }
        public int TiempoJugado { get; set; }
        public long Monedas { get; set; }
        public bool EstaSilenciado { get; set; }
        public IEntidadCombate EntidadCombate { get; set; }
        public Inventario Inventario { get; set; }       

        public Guerrero(long idPersonaje, GuerreroEntidadCombate entidadCombate, Inventario inventario)
        {
            IdPersonaje = idPersonaje;
            EntidadCombate = entidadCombate;
            Inventario = inventario;
        }

        public byte Clase() { return (byte)Tipos.Clase.GUERRERO; }
    }
}