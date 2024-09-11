namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_AumentoCantidadItemInventario : IGestorMensaje
    {
        public void GestionarMensaje(string mensaje)
        {
            ME_AumentoCantidadItemInventario me = Deserializador.Deserializar<ME_AumentoCantidadItemInventario>(mensaje);

            if(me != null)
            {
                GestorDeEventos.AumentoCantidadItemInventario.Invoke(me.SeccionInventario, me.CantidadAumentadad, me.Ranura);
            }
        }
    }
}