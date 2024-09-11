using Assets.Scripts.Personajes;

public interface IPersonaje
{
    long IdPersonaje { get; set; }
    byte Peinado { get; set; }
    byte AspectoFacial { get; set; }
    bool EsAdmin { get; set; }
    int TiempoJugado { get; set; }
    long Monedas { get; set; }
    bool EstaSilenciado { get; set; }
    IEntidadCombate EntidadCombate { get; set; }
    Inventario Inventario { get; set; }
    byte Clase();
}