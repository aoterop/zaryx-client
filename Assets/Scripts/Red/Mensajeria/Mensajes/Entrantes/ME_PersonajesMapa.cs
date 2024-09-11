using Assets.Scripts.Personajes;
using System.Collections.Generic;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_PersonajeMapa
    {
        public Guerrero GuerreroMapa { get; set; }
        public Tirador TiradorMapa { get; set; }
        public List<Nodo> Nodos { get; set; }

        public ME_PersonajeMapa() { }
    }
}