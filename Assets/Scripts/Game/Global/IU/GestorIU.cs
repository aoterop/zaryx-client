using Assets.Scripts.Game.Modelos.Tiendas;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorIU : MonoBehaviour
{
    public static GestorIU Instancia { get; private set; }

    public TextMeshProUGUI Nombre;
    public TextMeshProUGUI Nivel;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;
    public Image barraExp;
    public TextMeshProUGUI PorcentajeExp;
    public GameObject Chat;
    public GameObject MiembroGrupo1;
    public GameObject MiembroGrupo2;
    public GameObject Inventario;
    public GameObject EntidadSeleccionada;
    public GameObject PanelTienda;
    public GameObject InfoItem;
    public TextMeshProUGUI NombreMapa;
    public GameObject MiPersonaje;
    public GameObject TiempoJugado;
    public RectTransform SitioNotificaciones;
    public GameObject Notificacion;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += EscenaCargada;
    }

    public void NotificacionNormal()
    {
        GameObject gO = Instantiate(Notificacion, SitioNotificaciones.transform);
        gO.GetComponent<GestorNotificacion>().InstanciarMensajeNormal();
        Destroy(gO, 2f);
    }

    public void NotificacionGlobal()
    {
        GameObject gO = Instantiate(Notificacion, SitioNotificaciones.transform);
        gO.GetComponent<GestorNotificacion>().InstanciarMensajeGlobal();
        Destroy(gO, 2f);
    }

    public void NotificacionPrivada()
    {
        GameObject gO = Instantiate(Notificacion, SitioNotificaciones.transform);
        gO.GetComponent<GestorNotificacion>().InstanciarMensajePrivado();
        Destroy(gO, 2f);
    }

    public void IniciarTiempoJugado(int segundosTotales)
    {
        TiempoJugado.GetComponent<GestorTiempoJugado>().InicializarTemporizador(segundosTotales);
    }

    public void HabilitarChat()
    {
        Chat.GetComponent<GestorChat>().HabilitarChat();
    }

    public void CerrarChat()
    {
        Chat.GetComponent<GestorChat>().CerrarChat();
    }

    public void Susurrar(MensajeChat mensaje)
    {
        Chat.GetComponent<GestorChat>().Susurrar(mensaje);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {// Comprueba si el inventario ya está oculto

            if (!Chat.GetComponent<GestorChat>().Escribiendo)
            {
                Inventario.GetComponent<GestorInventario>().VisibilidadInventario();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelTienda.GetComponent<GestorPanelTienda>().OcultarPanelTienda();
            Inventario.GetComponent<GestorInventario>().OcultarInventario();
            Chat.GetComponent<GestorChat>().CerrarChat();
            TiempoJugado.GetComponent<GestorTiempoJugado>().Cerrar();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!Chat.GetComponent<GestorChat>().Escribiendo)
            {
                TiempoJugado.GetComponent<GestorTiempoJugado>().VisibilidadTiempoJugado();
            }
        }
    }

    public void EstablecerAvatar(byte clase)
    {
        switch(clase)
        {
            case (byte)Tipos.Clase.GUERRERO: { MiPersonaje.GetComponent<GestorAvatar>().EstablecerAvatarGuerrero(); } break;
            case (byte)Tipos.Clase.TIRADOR: { MiPersonaje.GetComponent<GestorAvatar>().EstablecerAvatarTirador(); } break;
            default: { } break;
        }
    }

    public void CerrarPersonaje()
    {
        GestorPersonaje.Instancia.CerrarPersonaje();
    }
    public void EstablecerMonedas(long monedas)
    {
        Inventario.GetComponent<GestorInventario>().EstablecerMonedas(monedas);
    }

    public void AbrirPanelTienda(Tienda t)
    {
        PanelTienda.GetComponent<GestorPanelTienda>().AbrirTienda(t);
        Inventario.GetComponent<GestorInventario>().MostrarInventario();
        Inventario.GetComponent<GestorInventario>().CentrarConTienda();
    }

    public void CerrarPanelTienda()
    {
        PanelTienda.GetComponent<GestorPanelTienda>().OcultarPanelTienda();
    }

    public void HabilitarPanelTienda()
    {
        PanelTienda.GetComponent<GestorPanelTienda>().Habilitar();
    }


    public void HabilitarInventario()
    {
        Inventario.GetComponent<GestorInventario>().Habilitar();
    }

    public void CargarItemConsumo(short refItem, short cantidad, byte ranura)
    {
        Inventario.GetComponent<GestorInventario>().AgregarItemConsumo(refItem, cantidad, ranura);
    }

    public void CargarItemEquipo(short refItem, short cantidad, byte ranura)
    {
        Inventario.GetComponent<GestorInventario>().AgregarItemEquipo(refItem, cantidad, ranura);
    }

    public void CargarMaestria(short refItem, short cantidad, byte ranura)
    {
        Inventario.GetComponent<GestorInventario>().AgregarMaestria(refItem, cantidad, ranura);
    }

    public void CargarMiscelanea(short refItem, short cantidad, byte ranura)
    {
        Inventario.GetComponent<GestorInventario>().AgregarMiscelanea(refItem, cantidad, ranura);
    }

    public void EstablecerNombre(string nombre)
    {
        Nombre.text = nombre;
    }

    public void EstablecerNivel(byte nivel)
    {
        Nivel.text = nivel.ToString();
    }

    public void EstablecerHp(int hpActual, int maxHp)
    {
        Hp.text = hpActual.ToString() + " / " + maxHp.ToString();
    }

    public void EstablecerMp(int mpActual, int maxMp)
    {
        Mp.text = mpActual.ToString() + " / " + maxMp.ToString();
    }

    public void EstablecerNombreMapa(string nombreMapa) { NombreMapa.text = nombreMapa; }

    public void MostrarChat() { Chat.SetActive(true); }

    public void OcultarChat() { Chat.SetActive(false); }

    public void MostrarMiembro1() { MiembroGrupo1.SetActive(true); }

    public void OcultarMiembro1() { MiembroGrupo1.SetActive(false); }

    public void MostrarInformacionItem(ItemBase datosItem)
    {
        InfoItem.GetComponent<GestorInfoItem>().RellenarDatos(datosItem);
    }

    public void OcultarInformacionItem()
    {
        InfoItem.GetComponent<GestorInfoItem>().OcultarInfoItem();
    }

    private void EscenaCargada(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == "Personajes" || escena.name == "Login")
        {
            Instancia = null;
            Destroy(gameObject);
        }
        else
        {
            EstablecerNombreMapa(GestorMapas.Instancia.NombresMapas[GestorPersonaje.Instancia.Personaje.EntidadCombate.Mapa]);
        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= EscenaCargada;
    }
}