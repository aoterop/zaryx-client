namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_RecibirTiendasMapa : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_RecibirTiendasMapa me = Deserializador.Deserializar<ME_RecibirTiendasMapa>(json);

            if(me != null)
            {
                GestorDeEventos.InstanciarTiendasMapa.Invoke(me.TiendasMapa);
            }
        }
    }
}