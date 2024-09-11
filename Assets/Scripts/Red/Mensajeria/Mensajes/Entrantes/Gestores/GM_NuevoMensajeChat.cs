namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_NuevoMensajeChat : IGestorMensaje
    {
        public void GestionarMensaje(string mensaje)
        {
            ME_NuevoMensajeChat me = Deserializador.Deserializar<ME_NuevoMensajeChat>(mensaje);

            if(me != null)
            {
                GestorDeEventos.NuevoMensajeChat.Invoke(me.Mensaje);
            }
        }
    }
}