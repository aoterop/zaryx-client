namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_NuevoItemInventario : IGestorMensaje
    {
        public void GestionarMensaje(string mensaje)
        {
            ME_NuevoItemInventario me = Deserializador.Deserializar<ME_NuevoItemInventario>(mensaje);

            if(me != null) 
            {
                GestorDeEventos.NuevoItemInventario.Invoke(me.SeccionInventario, me.IdItem, me.Cantidad, me.Ranura);
            }
        }
    }
}