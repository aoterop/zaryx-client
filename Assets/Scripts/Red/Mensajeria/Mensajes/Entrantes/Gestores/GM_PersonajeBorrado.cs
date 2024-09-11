namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_PersonajeBorrado : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_PersonajeBorrado mensaje = Deserializador.Deserializar<ME_PersonajeBorrado>(json);

            if(mensaje != null)
            {
                if(mensaje.Borrado)
                {// El personaje se pudo borrar exitosamente.
                    GestorDeEventos.BorrarPersonaje?.Invoke(mensaje.IdPersonaje);
                }
            }
        }

    }
}