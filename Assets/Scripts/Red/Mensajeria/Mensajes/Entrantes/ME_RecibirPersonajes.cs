using Assets.Scripts.Personajes;
using System.Collections.Generic;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes
{
    public class ME_RecibirPersonajes
    {
        public List<Guerrero> Guerreros { get; set; }
        public List<Tirador> Tiradores { get; set; }
        
        public ME_RecibirPersonajes() { }
    }
}