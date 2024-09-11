namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    internal class GM_CrearPersonaje : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_CrearPersonaje mensaje = Deserializador.Deserializar<ME_CrearPersonaje>(json);

            if (mensaje != null)
            {
                if (mensaje.NombreEnUso)
                {// El nombre del personaje ya está en uso.
                    GestorDeEventos.NombrePersonajeEnUso?.Invoke();
                }
                else
                {// ¡El personaje ha sido creado!
                    GestorDeEventos.PersonajeCreado?.Invoke();
                }
            }
        }
    }
}