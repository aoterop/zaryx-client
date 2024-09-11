namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_InstanciaItemMapa : IGestorMensaje
    {
        public void GestionarMensaje(string mensaje)
        {
            ME_InstanciaItemMapa me = Deserializador.Deserializar<ME_InstanciaItemMapa>(mensaje);

            if(me != null)
            {
                GestorDeEventos.InstanciarItemMapa.Invoke(me.IdItemSuelo, me.IdItem, me.Cantidad, me.X, me.Y);
            }
        }
    }
}