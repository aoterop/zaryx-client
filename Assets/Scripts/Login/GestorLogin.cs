using System.Collections;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorLogin : MonoBehaviour
{
    public GameObject panelError;
    public TextMeshProUGUI mensajeError;

    public TMP_InputField usuario;
    public TMP_InputField contra;

    public Button btnEntrar;

    private bool esperandoRespuesta = false;
    private bool UsuarioSeleccionado = false;
    private bool ContraSeleccionada = false;

    private void Awake()
    {
        usuario.onSelect.AddListener(SeleccionUsuario);
        usuario.onDeselect.AddListener(DeseleccionUsuario);

        contra.onSelect.AddListener(SeleccionContra);
        contra.onDeselect.AddListener(DeseleccionContra);

        GestorDeEventos.CuentaInexistente += CuentaInexistente;
        GestorDeEventos.CredencialesIncorrectas += CredencialesIncorrectas;
        GestorDeEventos.LoginExitoso += LoginExitoso;
        GestorDeEventos.CuentaEnUso += CuentaEnUso;
        GestorDeEventos.CuentaInactiva += CuentaInactiva;
        GestorDeEventos.CuentaBaneada += CuentaBaneada;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(UsuarioSeleccionado)
            {
                contra.ActivateInputField();
                UsuarioSeleccionado = false;
                ContraSeleccionada = true;
            }
            else
            {
                UsuarioSeleccionado = true;
                ContraSeleccionada = false;
                usuario.ActivateInputField();
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(ContraSeleccionada)
            {
                IniciarSesion();
            }
        }
    }

    public void SeleccionUsuario(string arg0)
    {
        UsuarioSeleccionado = true;
    }

    public void DeseleccionUsuario(string arg0)
    {
        UsuarioSeleccionado = false;
    }

    public void SeleccionContra(string arg0)
    {
        ContraSeleccionada = true;
    }

    public void DeseleccionContra(string arg0)
    {
        ContraSeleccionada = false;
    }

    public void EditandoContra(string arg0)
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            usuario.ActivateInputField();
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            IniciarSesion();
        }
    }

    public void IniciarSesion()
    {
        // Comprobación de campos.
        if(CredencialesCorrectas(usuario.text, contra.text))
        {
            if (!GestorDeRed.Red.ObtenerEstadoConexion())
            {// No hay conexión con el servidor.
                GestorDeEventos.ConectarConServidor?.Invoke();
                FalloDeRed();
            }
            else
            {// El cliente está conectado correctamente al servidor.
                
                if(!esperandoRespuesta)
                {
                    // Enviar mensaje de autenticación.
                    MS_Login mensaje = new(usuario.text, Encriptador.ObtenerSHA512(contra.text));
                    Emisor.Enviar(mensaje.Tipo(), Serializador.Serializar(mensaje));
                }

                if (GestorDeRed.Red.ObtenerEstadoConexion())
                {// Bloqueo para evitar reintentos mientras se procesa el mensaje en el servidor.              
                    esperandoRespuesta = true;
                }
                else
                {
                    FalloDeRed();
                }
            }
        }
        else
        {// Las credenciales no son correctas.
            MostrarPanelDeError(Alertas.Mensajes["CredencialesIncorrectas"]);
        }
    }

    private void CuentaInactiva()
    {
        MostrarPanelDeError(Alertas.Mensajes["CuentaInactiva"]);
        HabilitarBoton();
    }

    private void FalloDeRed()
    {
        MostrarPanelDeError(Alertas.Mensajes["ConexionPerdida"]);
        HabilitarBoton();
    }
    private void CuentaBaneada()
    {
        MostrarPanelDeError(Alertas.Mensajes["CuentaBaneada"]);
        HabilitarBoton();
    }

    private void CuentaEnUso()
    {
        MostrarPanelDeError(Alertas.Mensajes["CuentaEnUso"]);
        HabilitarBoton();
    }

    private void MostrarPanelDeError(string mensajeDeError)
    {
        panelError.SetActive(true);
        mensajeError.text = mensajeDeError;
    }

    private bool CredencialesCorrectas(string usuario, string contra)
    {
        return UsuarioValido(usuario) && ContraValida(contra);
    }

    private void LoginExitoso()
    {
        GestorDeEventos.CambiarEscena.Invoke("Personajes");
    }

    private void CuentaInexistente()
    {
        MostrarPanelDeError(Alertas.Mensajes["CuentaInexistente"]);
        HabilitarBoton();
    }

    private void CredencialesIncorrectas()
    {
        MostrarPanelDeError(Alertas.Mensajes["CredencialesIncorrectas"]);
        HabilitarBoton();
    }

    private bool UsuarioValido(string usuario)
    {
        // Debe tener una longitud entre [4, 14].
        if (usuario.Length < 4 || usuario.Length > 14) { return false; }

        // No debe contener espacios en blanco.
        if (usuario.Contains(' ')) { return false; }

        // No debe contener caracteres no utf8.
        if (Regex.IsMatch(usuario, @"[\x00-\x08\x0E-\x1F]")) { return false; }

        // No debe contener caracteres no alfanuméricos.
        if (!Regex.IsMatch(usuario, @"^[a-zA-Z0-9]+$")) { return false; }

        return true;
    }

    private bool ContraValida(string contra)
    {
        // Longitud entre [8, 20].
        if (contra.Length < 8 || contra.Length > 20) { return false; }

        // No debe contener espacios en blanco.
        if (contra.Contains(' ')) { return false; }

        // No debe contener caracteres no utf8.
        if (Regex.IsMatch(contra, @"[\x00-\x08\x0E-\x1F]")) { return false; }

        // Debe contener al menos un número y una letra.
        if (!Regex.IsMatch(contra, @"\d") || !Regex.IsMatch(contra, @"[a-zA-Z]")) { return false; }

        return true;
    }

    public void HabilitarBoton()
    {
        esperandoRespuesta = false;
       //btnEntrar.interactable = true;
    }

    private void OnDestroy()
    {
        usuario.onEndEdit.RemoveAllListeners();
        contra.onEndEdit.RemoveAllListeners();

        GestorDeEventos.CuentaInexistente -= CuentaInexistente;
        GestorDeEventos.CredencialesIncorrectas -= CredencialesIncorrectas;
        GestorDeEventos.LoginExitoso -= LoginExitoso;
        GestorDeEventos.CuentaEnUso -= CuentaEnUso;
        GestorDeEventos.CuentaInactiva -= CuentaInactiva;
        GestorDeEventos.CuentaBaneada -= CuentaBaneada;
    }
}