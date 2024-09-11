using Assets.Scripts.Personajes;

namespace Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores
{
    public class GM_RecibirPersonajes : IGestorMensaje
    {
        public void GestionarMensaje(string json)
        {
            ME_RecibirPersonajes mensaje = Deserializador.Deserializar<ME_RecibirPersonajes>(json);

            if (mensaje != null)
            {
                foreach (Guerrero g in mensaje.Guerreros)
                {
                    GestorDeEventos.RellenarGuerrero?.Invoke(g);
                }

                foreach(Tirador t in mensaje.Tiradores)
                {
                    GestorDeEventos.RellenarTirador?.Invoke(t);
                }
            }
        }
    }
}