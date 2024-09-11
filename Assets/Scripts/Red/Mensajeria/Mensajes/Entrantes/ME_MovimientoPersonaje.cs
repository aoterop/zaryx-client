using System.Collections.Generic;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_MovimientoPersonaje
    {
        public List<Nodo> Nodos { get; set; }
        public long IdPersonaje { get; set; }

        public ME_MovimientoPersonaje() { }
    }
}