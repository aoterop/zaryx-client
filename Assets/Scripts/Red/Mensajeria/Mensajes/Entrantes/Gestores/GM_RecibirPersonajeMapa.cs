using Assets.Scripts.Personajes;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_RecibirPersonajeMapa : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_PersonajeMapa me = Deserializador.Deserializar<ME_PersonajeMapa>(json);

            if (me != null)
            {
                if(me.GuerreroMapa != null) { GestorDeEventos.InstanciarNuevoPersonajeMapa.Invoke(me.GuerreroMapa, me.Nodos); }

                if(me.TiradorMapa != null) { GestorDeEventos.InstanciarNuevoPersonajeMapa.Invoke(me.TiradorMapa, me.Nodos); }
            }
        }
    }
}