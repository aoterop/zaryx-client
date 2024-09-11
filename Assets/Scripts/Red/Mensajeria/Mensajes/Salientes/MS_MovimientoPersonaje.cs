using System.Collections.Generic;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_MovimientoPersonaje : IMensajeSaliente
    {
        public List<Nodo> Nodos { get; set; }
        public short IdMapa { get; set; }
        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_MOVIMIENTO_PERSONAJE; }

        public MS_MovimientoPersonaje(List<Nodo> nodos, short idMapa)
        {
            Nodos = nodos;
            IdMapa = idMapa;
        }
    }
}