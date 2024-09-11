namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_SalidaPersonaje : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_SalidaPersonaje me = Deserializador.Deserializar<ME_SalidaPersonaje>(json);

            if(me != null)
            {
                GestorDeEventos.EliminarPersonajeDeMapa.Invoke(me.IdPersonaje);
            }
        }
    }
}