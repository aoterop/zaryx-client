using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorChat : MonoBehaviour
{
    public bool Activado { get; set; }
    public bool Escribiendo { get; set; }

    public GameObject MensajeNormal;
    public GameObject MensajeGlobal;
    public GameObject MensajePrivado;

    public Button BotonAutoScroll;
    public ScrollRect Scroll;

    public GameObject Mensajes;

    public TMP_InputField InputMensaje;

    private bool AutoScroll;

    private void Awake()
    {
        GestorDeEventos.NuevoMensajeChat += InstanciarMensaje;

        Activado = false;
        Escribiendo = false;
        AutoScroll = false;

        InputMensaje.onEndEdit.AddListener(EditandoMensaje);
        InputMensaje.onSelect.AddListener(Seleccionado);
        InputMensaje.onDeselect.AddListener(Deseleccionado);
    }

    private void Start()
    {
        EstablecerAutoScroll();
    }


    public void EstablecerAutoScroll()
    {
        ColorBlock colorBlock = BotonAutoScroll.colors;

        if (AutoScroll)
        {
            colorBlock.normalColor = UnityEngine.Color.white;
            BotonAutoScroll.colors = colorBlock;
        }
        else
        {
            colorBlock.normalColor = new UnityEngine.Color(1f, 0.647f, 0f);
            BotonAutoScroll.colors = colorBlock;
        }

        AutoScroll = !AutoScroll;

        AjustarScroll();
    }

    public void AjustarScroll()
    {
        if (AutoScroll)
        {
            Scrollbar scrollbar = Scroll.verticalScrollbar;
            scrollbar.value = 0f;
            Scroll.verticalNormalizedPosition = 0f;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Scroll.content);
            scrollbar.value = 0f;
            Scroll.verticalNormalizedPosition = 0f;
        }
    }

    private void EditandoMensaje(string arg0)
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {// Se envía el mensaje.            
            if(Activado)
            {
                EnviarMensajeAlChat();
            }
        }
    }

    private void Seleccionado(string arg0)
    {
        Escribiendo = true;
        InputMensaje.ActivateInputField();
        Deseleccionar();
    }

    private void Deseleccionado(string arg0)
    {
        Escribiendo = false;
    }

    public void Susurrar(MensajeChat mensaje)
    {
        InputMensaje.text = "/" + mensaje.NombreEmisor + " ";
        InputMensaje.ActivateInputField();

        Deseleccionar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Activado && !Escribiendo)
            {
                CerrarChat();
            }
            else
            {
                AbrirChat();
            }
        }
    }

    public void InstanciarMensaje(MensajeChat mensaje)
    {
        switch (mensaje.Tipo)
        {
            case (byte)Tipos.MensajeChat.NORMAL:
                {
                    GameObject m = Instantiate(MensajeNormal, Mensajes.transform);
                    m.GetComponent<Mensaje>().Inicializar(mensaje, false);
                    AjustarScroll();

                    if (!Activado) { GestorIU.Instancia.NotificacionNormal(); }
                }
                break;

            case (byte)Tipos.MensajeChat.GLOBAL:
                {
                    GameObject m = Instantiate(MensajeGlobal, Mensajes.transform);
                    m.GetComponent<Mensaje>().Inicializar(mensaje, true);
                    AjustarScroll();

                    if (!Activado) { GestorIU.Instancia.NotificacionGlobal(); }
                }
                break;

            case (byte)Tipos.MensajeChat.PRIVADO:
                {
                    GameObject m = Instantiate(MensajePrivado, Mensajes.transform);
                    m.GetComponent<Mensaje>().Inicializar(mensaje, false);
                    AjustarScroll();

                    if (!Activado) { GestorIU.Instancia.NotificacionPrivada(); }
                }
                break;

            default: { } break;
        }
    }

    public void EnviarMensajeAlChat()
    {
        string mensaje = InputMensaje.text;
        string contenido, receptor = "";
        byte tipo;

        if (mensaje.Trim().Length > 0)
        {// Si el mensaje no está vacío

            if (mensaje.StartsWith("#"))
            {// Mensaje global.

                contenido = mensaje[1..];

                if(contenido.Trim().Length > 0)
                {
                    tipo = (byte)Tipos.MensajeChat.GLOBAL;
                }
                else
                {
                    InputMensaje.text = "# ";
                    Deseleccionar();

                    return;
                }
            }
            else if (mensaje.StartsWith("/"))
            {
                int indiceEspacio = mensaje.IndexOf(' ');

                if (indiceEspacio >= 0 && indiceEspacio < mensaje.Length - 1)
                {
                    receptor = mensaje[1..indiceEspacio];
                    contenido = mensaje[(indiceEspacio + 1)..];
                    tipo = (byte)Tipos.MensajeChat.PRIVADO;
                }
                else
                {
                    InputMensaje.text = "/";
                    Deseleccionar();
                    return;
                }
            }
            else
            {// Mensaje normal.
                contenido = mensaje;
                tipo = (byte)Tipos.MensajeChat.NORMAL;
            }

            contenido = EliminarEspaciosEnBlanco(contenido);

            switch (tipo)
            {
                case (byte)Tipos.MensajeChat.NORMAL:
                    {
                        InputMensaje.text = "";
                        InputMensaje.ActivateInputField();

                        MS_MensajeChat ms = new(tipo, contenido, receptor);
                        Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                    }
                    break;

                case (byte)Tipos.MensajeChat.GLOBAL:
                    {
                        InputMensaje.text = "# ";
                        Deseleccionar();

                        MS_MensajeChat ms = new(tipo, contenido, receptor);
                        Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                    }
                    break;

                case (byte)Tipos.MensajeChat.PRIVADO:
                    {
                        InputMensaje.text = "/" + receptor + " ";
                        MensajeChat mChat = new(contenido, GestorPersonaje.Instancia.Personaje.IdPersonaje,
                            GestorPersonaje.Instancia.Personaje.EntidadCombate.Nombre, tipo,
                            GestorMapas.Instancia.NombresMapas[GestorPersonaje.Instancia.Personaje.EntidadCombate.Mapa]);

                        Deseleccionar();

                        if (receptor != GestorPersonaje.Instancia.Personaje.EntidadCombate.Nombre)
                        {
                            InstanciarMensaje(mChat);

                            MS_MensajeChat ms = new(tipo, contenido, receptor);
                            Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                        }
                    }
                    break;

                default: { } break;
            }
        }
    }

    public static string EliminarEspaciosEnBlanco(string texto)
    {
        int indiceInicial = 0;
        while (indiceInicial < texto.Length && texto[indiceInicial] == ' ')
        {
            indiceInicial++;
        }

        int indiceFinal = texto.Length - 1;
        while (indiceFinal >= 0 && texto[indiceFinal] == ' ')
        {
            indiceFinal--;
        }

        return texto[indiceInicial..(indiceFinal + 1)];
    }


    private void Deseleccionar()
    {
        InputMensaje.selectionFocusPosition = InputMensaje.text.Length;
        InputMensaje.selectionAnchorPosition = InputMensaje.text.Length;
    }


    public void HabilitarChat()
    {
        CerrarChat();
    }

    public void AbrirChat()
    {
        CambiarVisibilidadHijos(gameObject, 1f, true);
        Activado = true;

        StartCoroutine(DeseleccionarTrasApertura());
    }

    public IEnumerator DeseleccionarTrasApertura()
    {
        yield return new WaitForEndOfFrame();
        InputMensaje.ActivateInputField();
        Deseleccionar();
    }

    public void CerrarChat()
    {
        CambiarVisibilidadHijos(gameObject, 0f, false);
        Activado = false;
    }

    public void CambiarVisibilidadHijos(GameObject parentObject, float alpha, bool raycast)
    {
        Image[] imagenes = parentObject.GetComponentsInChildren<Image>(true);
        foreach (Image imagen in imagenes)
        {
            UnityEngine.Color color = imagen.color;
            color.a = alpha;
            imagen.color = color;
            imagen.raycastTarget = raycast;
        }

        RawImage[] imagenesRaw = parentObject.GetComponentsInChildren<RawImage>(true);
        foreach (RawImage imagenRaw in imagenesRaw)
        {
            UnityEngine.Color color = imagenRaw.color;
            color.a = alpha;
            imagenRaw.color = color;
            imagenRaw.raycastTarget = raycast;
        }

        TextMeshProUGUI[] textos = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI texto in textos)
        {
            UnityEngine.Color color = texto.color;
            color.a = alpha;
            texto.color = color;
            texto.raycastTarget = raycast;
        }
    }

    public void OnDestroy()
    {
        GestorDeEventos.NuevoMensajeChat -= InstanciarMensaje;
        InputMensaje.onEndEdit.RemoveAllListeners();
        InputMensaje.onSelect.RemoveAllListeners();
        InputMensaje.onDeselect.RemoveAllListeners();
    }
}