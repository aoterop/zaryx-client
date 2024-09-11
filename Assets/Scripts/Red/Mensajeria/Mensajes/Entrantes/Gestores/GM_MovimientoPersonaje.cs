namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_MovimientoPersonaje : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_MovimientoPersonaje me = Deserializador.Deserializar<ME_MovimientoPersonaje>(json);

            if(me != null && me.Nodos != null)
            {               
                GestorDeEventos.MovimientoPersonaje.Invoke(me.IdPersonaje, me.Nodos);
            }
        }
    }
}