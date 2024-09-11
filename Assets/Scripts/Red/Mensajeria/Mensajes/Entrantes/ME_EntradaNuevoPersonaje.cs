using Assets.Scripts.Personajes;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_EntradaNuevoPersonaje
    {
        public Guerrero GuerreroNuevo { get; set; }
        public Tirador TiradorNuevo { get; set; }
        public ME_EntradaNuevoPersonaje() { }
    }
}