public class GM_Login : IGestorMensaje
{
    public void GestionarMensaje(string json)
    {
        ME_Login mensaje = Deserializador.Deserializar<ME_Login>(json);

        if(mensaje != null)
        {
            switch(mensaje.EstadoCuenta)
            {
                case (byte)Tipos.EstadoCuenta.NO_EXISTE: { GestorDeEventos.CuentaInexistente?.Invoke(); } break;
                case (byte)Tipos.EstadoCuenta.CREDENCIALES_INCORRECTAS: { GestorDeEventos.CredencialesIncorrectas?.Invoke(); } break;
                case (byte)Tipos.EstadoCuenta.CREDENCIALES_CORRECTAS: { GestorDeEventos.LoginExitoso?.Invoke(); } break;
                case (byte)Tipos.EstadoCuenta.INACTIVA: { GestorDeEventos.CuentaInactiva?.Invoke(); } break;
                case (byte)Tipos.EstadoCuenta.BANEADA: { GestorDeEventos.CuentaBaneada?.Invoke(); } break;
                case (byte)Tipos.EstadoCuenta.EN_USO: { GestorDeEventos.CuentaEnUso?.Invoke(); } break;              
            }
        }
    }
}