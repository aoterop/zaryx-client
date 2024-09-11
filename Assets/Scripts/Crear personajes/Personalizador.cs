using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Tipos;

public class Personalizador : MonoBehaviour
{
    private int numAspectosFaciales = Enum.GetValues(typeof(AspectoFacial)).Length;
    private int numEstilosPeinado = Enum.GetValues(typeof(EstiloPeinado)).Length;
    private AspectoFacial aspecto_facial;
    private EstiloPeinado estilo_peinado;
    private Clase clase;

    public GameObject panelError;
    public TextMeshProUGUI mensajeError;
    public GestorApariencia apariencia;
    public TMP_InputField nombre;
    public TextMeshProUGUI tipoCara;
    public TextMeshProUGUI tipoPeinado;
    public Button btnCrear;

    private void Awake()
    {
        GestorDeEventos.NombrePersonajeEnUso += PersonajeEnUso;
        GestorDeEventos.PersonajeCreado += PersonajeCreado;
    }

    void Start()
    {
        aspecto_facial = AspectoFacial.AspectoFacial1;
        estilo_peinado = EstiloPeinado.Peinado1;
        clase = Clase.GUERRERO;
    }

    private void PersonajeEnUso()
    {
        MostrarPanelDeError(Alertas.Mensajes["NombreEnUso"]);
        HabilitarBotonCrear();
    }

    private void PersonajeCreado()
    {
        GestorDeEventos.CambiarEscena.Invoke("Personajes");
    }

    public void SiguienteAspectoFacial()
    {
        int siguienteIndice = (int)aspecto_facial + 1;

        if(siguienteIndice > numAspectosFaciales)
        {
            siguienteIndice = (int)AspectoFacial.AspectoFacial1;
        }

        aspecto_facial = (AspectoFacial)siguienteIndice;
        AplicarAspectoFacial(aspecto_facial);
    }

    public void AspectoFacialAnterior()
    {
        int anteriorIndice = (int)aspecto_facial - 1;

        if(anteriorIndice == 0)
        {
            anteriorIndice = numAspectosFaciales;
        }

        aspecto_facial = (AspectoFacial)anteriorIndice;
        AplicarAspectoFacial(aspecto_facial);
    }

    void AplicarAspectoFacial(AspectoFacial aspectoFacial)
    {
        tipoCara.text = "Tipo " + ((byte)aspectoFacial).ToString();
        apariencia.Equipar(ComponentePersonaje.CARA, "Cara" + ((byte)aspectoFacial).ToString());
    }

    public void SiguienteEstiloPeinado()
    {
        int siguienteIndice = (int)estilo_peinado + 1;

        if(siguienteIndice > numEstilosPeinado)
        {
            siguienteIndice = (int)EstiloPeinado.Peinado1;
        }

        estilo_peinado = (EstiloPeinado)siguienteIndice;
        AplicarEstiloPeinado(estilo_peinado);
    }

    public void EstiloPeinadoAnterior()
    {
        int anteriorIndice = (int)estilo_peinado - 1;

        if(anteriorIndice == 0)
        {
            anteriorIndice = numEstilosPeinado;
        }

        estilo_peinado = (EstiloPeinado)anteriorIndice;
        AplicarEstiloPeinado(estilo_peinado);
    }

    void AplicarEstiloPeinado(EstiloPeinado estiloPeinado)
    {
        tipoPeinado.text = "Peinado " + ((byte)estiloPeinado).ToString();
        apariencia.Equipar(ComponentePersonaje.PEINADO, "Peinado" + ((byte)estiloPeinado).ToString());
    }

    public void SeleccionarGuerrero()
    {
        clase = Clase.GUERRERO;
        apariencia.TransformarGuerreroSM();
    }

    public void SeleccionarTirador()
    {
        clase = Clase.TIRADOR;
        apariencia.TransformarTiradorSM();
    }

    public void SeleccionarMago()
    {
        clase = Clase.MAGO;
        apariencia.TransformarMagoSM();
    }

    private bool NombreValido(string nombrePersonaje)
    {
        // Debe tener una longitud entre [4, 14].
        if (nombrePersonaje.Length < 4 || nombrePersonaje.Length > 14) { return false; }

        // No debe contener espacios en blanco.
        if (nombrePersonaje.Contains(' ')) { return false; }

        // No debe contener caracteres no utf8.
        if (Regex.IsMatch(nombrePersonaje, @"[\x00-\x08\x0E-\x1F]")) { return false; }

        // No debe contener caracteres no alfanuméricos.
        if (!Regex.IsMatch(nombrePersonaje, @"^[a-zA-Z0-9]+$")) { return false; }

        return true;
    }

    private void MostrarPanelDeError(string mensajeDeError)
    {
        panelError.SetActive(true);
        mensajeError.text = mensajeDeError;
    }

    private void HabilitarBotonCrear()
    {
        btnCrear.interactable = true;
    }

    public void CrearPersonaje()
    {
        if(!panelError.activeSelf)
        {
            if (NombreValido(nombre.text))
            {
                btnCrear.interactable = false;
                MS_CrearPersonaje mensaje = new(nombre.text, (byte)clase, (byte)estilo_peinado, (byte)aspecto_facial);
                Emisor.Enviar(mensaje.Tipo(), Serializador.Serializar(mensaje));
            }
            else
            {
                MostrarPanelDeError(Alertas.Mensajes["NombreInvalido"]);
            }
        }      
    }

    public void Volver()
    {
        GestorDeEventos.CambiarEscena.Invoke("Personajes");
    }
    private void OnDestroy()
    {
        GestorDeEventos.NombrePersonajeEnUso -= PersonajeEnUso;
        GestorDeEventos.PersonajeCreado -= PersonajeCreado;
    }
}