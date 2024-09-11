namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_EliminarItemMapa : IGestorMensaje
    {
        public void GestionarMensaje(string mensaje)
        {
            ME_EliminarItemMapa me = Deserializador.Deserializar<ME_EliminarItemMapa>(mensaje);

            if(me != null)
            {
                GestorDeEventos.EliminarItemMapa.Invoke(me.IdItemSuelo);
            }
        }
    }
}