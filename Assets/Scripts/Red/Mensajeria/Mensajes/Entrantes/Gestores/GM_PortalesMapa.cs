namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_PortalesMapa : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_PortalesMapa mensaje = Deserializador.Deserializar<ME_PortalesMapa>(json);

            if(mensaje != null)
            {
                if(mensaje.Portales != null)
                {  
                    foreach(var portal in mensaje.Portales)
                    {                       
                        GestorDeEventos.InstanciarPortal.Invoke(portal);
                        GestorMapas.Instancia.AEstrellasMapas[portal.MapaOrigen].Mapa[portal.OrigenX, portal.OrigenY].IdPortal = portal.IdPortal;
                    }
                }
            }
        }
    }
}