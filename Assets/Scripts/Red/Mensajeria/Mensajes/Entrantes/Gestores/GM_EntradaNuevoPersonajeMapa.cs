
using Assets.Scripts.Personajes;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_EntradaNuevoPersonajeMapa : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_EntradaNuevoPersonaje me = Deserializador.Deserializar<ME_EntradaNuevoPersonaje>(json);

            if(me != null)
            {
                if (me.GuerreroNuevo != null) { GestorDeEventos.InstanciarNuevoPersonajeMapa.Invoke(me.GuerreroNuevo, null); }
                if (me.TiradorNuevo != null) { GestorDeEventos.InstanciarNuevoPersonajeMapa.Invoke(me.TiradorNuevo, null); }
            }
        }
    }
}