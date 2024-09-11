namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_MonedasActuales : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_MonedasActuales me = Deserializador.Deserializar<ME_MonedasActuales>(json);

            if(me != null)
            {
                GestorDeEventos.MonedasActuales.Invoke(me.MonedasActuales);
            }
        }
    }
}