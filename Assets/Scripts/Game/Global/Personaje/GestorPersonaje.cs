using Assets.Scripts.Game.Portales;
using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GestorPersonaje : MonoBehaviour
{
    public static GestorPersonaje Instancia { get; private set; }
    public IPersonaje Personaje { get; set; } // Personaje a nivel lógico.
    public GameObject personajeObject; // Representación del personaje (GameObject).
    private GestorApariencia apariencia;
    private GestorEntidad entidad;
    private GestorAnimaciones animador;
    private GestorNombre nick;
    private GestorIU IU;
    private byte Clase { get; set; }
    private AEstrella aEstrella;
    //Movimiento
    // Camera, hit, tag
    public Camera camara;
    private RaycastHit hit;
    private readonly string tagSuelo = "Suelo";
    // RTS
    public GameObject rts;
    private GameObject rtsInstanciado;
    public float retardoDestruccionRts = 5.0f;
    // Físicas
    public float distanciaFrenado = 0.1f;
    public Vector2 posicionActual;
    public Vector3 posicionMarcada;
    // Nodos y estado
    public List<Nodo> nodos;
    private Coroutine movimiento = null;
    private bool hayNuevoObjetivo = false;

    private Vector2 coordenadas;

    private short NuevoMapaX = -1, NuevoMapaY = -1;
    GestorPortales gestorPortales;
    private short UltimoMapa = -1;
    private long IdItemSuelo = -1;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            Personaje = PersonajeSeleccionado.Instancia.Personaje;
            Clase = PersonajeSeleccionado.Instancia.Clase;
            apariencia = personajeObject.GetComponent<GestorApariencia>();
            entidad = personajeObject.GetComponent<GestorEntidad>();
            animador = personajeObject.GetComponent<GestorAnimaciones>();
            gestorPortales = FindObjectOfType<GestorPortales>();

            nick = personajeObject.GetComponentInChildren<GestorNombre>();
            nick.EstablecerNombre(Personaje.EntidadCombate.Nombre);

            NuevoMapaX = Personaje.EntidadCombate.X;
            NuevoMapaY = Personaje.EntidadCombate.Y;

            posicionActual = new Vector2(Personaje.EntidadCombate.X, Personaje.EntidadCombate.Y);
            aEstrella = GestorMapas.Instancia.AEstrellasMapas[Personaje.EntidadCombate.Mapa];

            switch (Clase)
            {
                case (byte)Tipos.Clase.GUERRERO:
                    {
                        apariencia.TransformarGuerreroSM();
                    }
                    break;

                case (byte)Tipos.Clase.TIRADOR:
                    {
                        apariencia.TransformarTiradorSM();
                    }
                    break;

                case (byte)Tipos.Clase.MAGO:
                    {
                        apariencia.TransformarMagoSM();
                    }
                    break;
                default: { } break;
            }            
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += EscenaCargada;
    }

    private void Start()
    {
        animador.EstablecerAnimator(Clase);
        apariencia.AplicarEstilos(Personaje.AspectoFacial, Personaje.Peinado);
        entidad.Posicionar(Personaje.EntidadCombate.X, Personaje.EntidadCombate.Y);
        personajeObject.SetActive(true);

        StartCoroutine(CargarUI());
        GestorEscena.Instancia.RegistrarGestor();
    }

    public void MovimientoSlotBasico(byte seccion, byte ranura1, byte ranura2)
    {
        switch (seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO:
                {
                    ItemPersonaje item = Personaje.Inventario.ItemsConsumo[ranura1].Clone();
                    Personaje.Inventario.ItemsConsumo.Remove(ranura1);
                    Personaje.Inventario.ItemsConsumo.Add(ranura2, item);

                } break;

            case (byte)Tipos.SeccionesInventario.EQUIPO:
                {
                    ItemPersonaje item = Personaje.Inventario.ItemsEquipo[ranura1].Clone();
                    Personaje.Inventario.ItemsEquipo.Remove(ranura1);
                    Personaje.Inventario.ItemsEquipo.Add(ranura2, item);

                }
                break;

            case (byte)Tipos.SeccionesInventario.MAESTRIA:
                {
                    ItemPersonaje item = Personaje.Inventario.Maestrias[ranura1].Clone();
                    Personaje.Inventario.Maestrias.Remove(ranura1);
                    Personaje.Inventario.Maestrias.Add(ranura2, item);

                }
                break;

            case (byte)Tipos.SeccionesInventario.MISCELANEA:
                {
                    ItemPersonaje item = Personaje.Inventario.Miscelanea[ranura1].Clone();
                    Personaje.Inventario.Miscelanea.Remove(ranura1);
                    Personaje.Inventario.Miscelanea.Add(ranura2, item);
                }
                break;

            default: { } break;
        }
    }

    public void RotationClasica(byte seccion, byte ranura1, byte ranura2)
    {
        switch (seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO:
                {
                    (Personaje.Inventario.ItemsConsumo[ranura1], Personaje.Inventario.ItemsConsumo[ranura2]) = (Personaje.Inventario.ItemsConsumo[ranura2], Personaje.Inventario.ItemsConsumo[ranura1]);
                    Personaje.Inventario.ItemsConsumo[ranura1].RanuraInventario = ranura2;
                    Personaje.Inventario.ItemsConsumo[ranura2].RanuraInventario = ranura1;
                }
                break;

            case (byte)Tipos.SeccionesInventario.EQUIPO:
                {
                    (Personaje.Inventario.ItemsEquipo[ranura1], Personaje.Inventario.ItemsEquipo[ranura2]) = (Personaje.Inventario.ItemsEquipo[ranura2], Personaje.Inventario.ItemsEquipo[ranura1]);
                    Personaje.Inventario.ItemsEquipo[ranura1].RanuraInventario = ranura2;
                    Personaje.Inventario.ItemsEquipo[ranura2].RanuraInventario = ranura1;
                }
                break;

            case (byte)Tipos.SeccionesInventario.MAESTRIA:
                {
                    (Personaje.Inventario.Maestrias[ranura1], Personaje.Inventario.Maestrias[ranura2]) = (Personaje.Inventario.Maestrias[ranura2], Personaje.Inventario.Maestrias[ranura1]);
                    Personaje.Inventario.Maestrias[ranura1].RanuraInventario = ranura2;
                    Personaje.Inventario.Maestrias[ranura2].RanuraInventario = ranura1;
                }
                break;

            case (byte)Tipos.SeccionesInventario.MISCELANEA:
                {
                    (Personaje.Inventario.Miscelanea[ranura1], Personaje.Inventario.Miscelanea[ranura2]) = (Personaje.Inventario.Miscelanea[ranura2], Personaje.Inventario.Miscelanea[ranura1]);
                    Personaje.Inventario.Miscelanea[ranura1].RanuraInventario = ranura2;
                    Personaje.Inventario.Miscelanea[ranura2].RanuraInventario = ranura1;
                }
                break;

            default: { } break;
        }
    }

    public void FusionSlots(byte seccion, byte ranura1, byte ranura2)
    {
        switch (seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO:
                {
                    Personaje.Inventario.ItemsConsumo[ranura2].Cantidad += Personaje.Inventario.ItemsConsumo[ranura1].Cantidad;
                    Personaje.Inventario.ItemsConsumo.Remove(ranura1);
                } break;


            case (byte)Tipos.SeccionesInventario.MISCELANEA: 
                {
                    Personaje.Inventario.Miscelanea[ranura2].Cantidad += Personaje.Inventario.Miscelanea[ranura1].Cantidad;
                    Personaje.Inventario.Miscelanea.Remove(ranura1);
                } break;

            default: { } break;
        }
    }

    public void AumentarItemInventario(byte seccion, short cantidad, byte ranura)
    {// A nivel lógico.
        switch (seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO: 
                { 
                    if(Personaje.Inventario.ItemsConsumo.ContainsKey(ranura)) 
                    {
                        Personaje.Inventario.ItemsConsumo[ranura].Cantidad += cantidad;
                    }
                } break;

            case (byte)Tipos.SeccionesInventario.EQUIPO:
                {
                    if (Personaje.Inventario.ItemsEquipo.ContainsKey(ranura))
                    {
                        Personaje.Inventario.ItemsEquipo[ranura].Cantidad += cantidad;
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.MAESTRIA:
                {
                    if (Personaje.Inventario.Maestrias.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Maestrias[ranura].Cantidad += cantidad;
                    }
                }
                break;


            case (byte)Tipos.SeccionesInventario.MISCELANEA:
                {
                    if (Personaje.Inventario.Miscelanea.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Miscelanea[ranura].Cantidad += cantidad;
                    }
                }
                break;

            default: { } break;
        }
    }

    public void AgregarItemInventario(byte seccion, short idItem, short cantidad, byte ranura)
    {// A nivel lógico.
        switch (seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO: 
                { 
                    if(!Personaje.Inventario.ItemsConsumo.ContainsKey(ranura))
                    {
                        Personaje.Inventario.ItemsConsumo.Add(ranura, new ItemPersonaje(idItem, cantidad, 0, 0, ranura));
                    }
                } break;

            case (byte)Tipos.SeccionesInventario.EQUIPO:
                {
                    if (!Personaje.Inventario.ItemsEquipo.ContainsKey(ranura))
                    {
                        Personaje.Inventario.ItemsEquipo.Add(ranura, new ItemPersonaje(idItem, cantidad, 0, 0, ranura));
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.MAESTRIA:
                {
                    if (!Personaje.Inventario.Maestrias.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Maestrias.Add(ranura, new ItemPersonaje(idItem, cantidad, 0, 0, ranura));
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.MISCELANEA:
                {
                    if (!Personaje.Inventario.Miscelanea.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Miscelanea.Add(ranura, new ItemPersonaje(idItem, cantidad, 0, 0, ranura));
                    }
                }
                break;
            default: { } break;
        }
    }

    public void EliminarItemInventario(byte seccion, byte ranura)
    {// A nivel lógico.
        switch(seccion)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO: 
                { 
                    if(Personaje.Inventario.ItemsConsumo.ContainsKey(ranura))
                    {
                        Personaje.Inventario.ItemsConsumo.Remove(ranura);
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.EQUIPO:
                {
                    if (Personaje.Inventario.ItemsEquipo.ContainsKey(ranura))
                    {
                        Personaje.Inventario.ItemsEquipo.Remove(ranura);
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.MAESTRIA:
                {
                    if (Personaje.Inventario.Maestrias.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Maestrias.Remove(ranura);
                    }
                }
                break;

            case (byte)Tipos.SeccionesInventario.MISCELANEA:
                {
                    if (Personaje.Inventario.Miscelanea.ContainsKey(ranura))
                    {
                        Personaje.Inventario.Miscelanea.Remove(ranura);
                    }
                }
                break;

            default: { } break;
        }
    }

    private IEnumerator CargarUI()
    {
        yield return new WaitForEndOfFrame();

        IU = GestorIU.Instancia;
        IU.EstablecerNombre(Personaje.EntidadCombate.Nombre);
        IU.EstablecerNivel(Personaje.EntidadCombate.Nivel);
        IU.EstablecerHp(Personaje.EntidadCombate.Hp, Personaje.EntidadCombate.MaxHp);
        IU.EstablecerMp(Personaje.EntidadCombate.Mp, Personaje.EntidadCombate.MaxMp);
        IU.EstablecerNombreMapa(GestorMapas.Instancia.NombresMapas[Personaje.EntidadCombate.Mapa]);
        IU.EstablecerAvatar(Personaje.Clase());
        IU.EstablecerMonedas(Personaje.Monedas);

        
        foreach(var itemConsumo in Personaje.Inventario.ItemsConsumo.Values)
        {
            IU.CargarItemConsumo(itemConsumo.ReferenciaItem, itemConsumo.Cantidad, itemConsumo.RanuraInventario);
        }

        foreach (var itemEquipo in Personaje.Inventario.ItemsEquipo.Values)
        {
            IU.CargarItemEquipo(itemEquipo.ReferenciaItem, itemEquipo.Cantidad, itemEquipo.RanuraInventario);
        }

        foreach (var maestria in Personaje.Inventario.Maestrias.Values)
        {
            IU.CargarMaestria(maestria.ReferenciaItem, maestria.Cantidad, maestria.RanuraInventario);
        }

        foreach (var miscelanea in Personaje.Inventario.Miscelanea.Values)
        {
            IU.CargarMiscelanea(miscelanea.ReferenciaItem, miscelanea.Cantidad, miscelanea.RanuraInventario);
        }

        IU.HabilitarInventario();
        IU.OcultarInformacionItem();
        IU.HabilitarPanelTienda();
        IU.HabilitarChat();
        IU.IniciarTiempoJugado(Personaje.TiempoJugado);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool cogerItem = false;
            // No se detectó una colisión con un objeto con collider, ahora revisamos eventos de puntero
            PointerEventData pointerEventData = new(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);


            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.CompareTag("Item"))
                {
                    coordenadas = result.gameObject.GetComponent<GestorItem>().ObtenerCoordenadasItem();
                    IdItemSuelo = result.gameObject.GetComponent<GestorItem>().IdItemSuelo;
                    cogerItem = true;
                    break;
                }
            }

            if (cogerItem)
            {
                if (GestorMapas.Instancia.AEstrellasMapas[Personaje.EntidadCombate.Mapa].Mapa[(int)coordenadas.x, (int)coordenadas.y].EsCaminable)
                {
                    if (EstaAUnaCasilla(posicionActual, coordenadas))
                    {
                        CogerObjeto();
                    }
                    else
                    {
                        if (movimiento != null)
                        {
                            hayNuevoObjetivo = true;
                        }
                        else
                        {
                            GenerarRuta(true);
                        }
                    }
                }
            }
            else
            {
                Ray ray = camara.ScreenPointToRay(Input.mousePosition);

                if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 50))
                {
                    if (hit.transform.CompareTag(tagSuelo))
                    {
                        coordenadas = ObtenerCoordenadas(hit.point);

                        try
                        {
                            if (GestorMapas.Instancia.AEstrellasMapas[Personaje.EntidadCombate.Mapa].Mapa[(int)coordenadas.x, (int)coordenadas.y].EsCaminable)
                            {
                                if (posicionActual != coordenadas && coordenadas.x != -1)
                                {
                                    if (movimiento != null)
                                    {
                                        hayNuevoObjetivo = true;
                                    }
                                    else
                                    {
                                        GenerarRuta(false);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }
    }

    public bool EstaAUnaCasilla(Vector2 origen, Vector2 destino)
    {
        return (Math.Abs(origen.x - destino.x) <= 1) && (Math.Abs(origen.y - destino.y) <= 1);
    }

    public void PosicionarEnNuevoMapa()
    {
        entidad.Posicionar(NuevoMapaX, NuevoMapaY);
        posicionActual.x = NuevoMapaX;
        posicionActual.y = NuevoMapaY;
    }

    public void NotificarCambioMapa()
    {
        MS_CambioMapa ms = new(Personaje.IdPersonaje, UltimoMapa, Personaje.EntidadCombate.Mapa, NuevoMapaX, NuevoMapaY);
        Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
    }

    private void GenerarRuta(bool hayObjetivo)
    {
        nodos = aEstrella.EncontrarRuta(aEstrella.Mapa[(int)posicionActual.x, (int)posicionActual.y], aEstrella.Mapa[(int)coordenadas.x, (int)coordenadas.y]);

        if (nodos != null && nodos.Count > 0)
        {
            if (rtsInstanciado != null)
            {
                Destroy(rtsInstanciado);
            }

            rtsInstanciado = Instantiate(rts, new Vector3(coordenadas.x / 2, 0, coordenadas.y / 2), Quaternion.identity);

            nodos.RemoveAt(0);

            if(hayObjetivo)
            {
                nodos.RemoveAt(nodos.Count- 1);
            }

            MS_MovimientoPersonaje ms = new(nodos, Personaje.EntidadCombate.Mapa);
            Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));

            hayNuevoObjetivo = false;
            movimiento = StartCoroutine(Movimiento(hayObjetivo));
        }
    }


    private IEnumerator Movimiento(bool hayObjetivo)
    {
        int index = 0;
        while (index < nodos.Count && !hayNuevoObjetivo)
        {
            animador.Play((byte)Tipos.Animaciones.MOVIMIENTO);

            posicionMarcada = new Vector3(nodos[index].X / 2f, 0f, nodos[index].Y / 2f); // A nivel físico.

            Vector3 eulerAngle = new(0f, ObtenerAnguloDeDireccion(ObtenerDireccion(posicionActual, new Vector2(posicionMarcada.x * 2, posicionMarcada.z * 2))), 0f);
            Quaternion rotation = Quaternion.Euler(eulerAngle);
            personajeObject.transform.rotation = rotation;

            float step = GestorPersonaje.Instancia.Personaje.EntidadCombate.Velocidad * Time.fixedDeltaTime;

            while (Vector3.Distance(personajeObject.transform.position, posicionMarcada) > distanciaFrenado)
            {
                personajeObject.transform.position = Vector3.MoveTowards(personajeObject.transform.position, posicionMarcada, step);
                yield return new WaitForFixedUpdate();
            }

            posicionActual.x = posicionMarcada.x * 2;
            posicionActual.y = posicionMarcada.z * 2;

            MS_ActualizarPosicion ms = new((short)posicionActual.x, (short)posicionActual.y, Personaje.IdPersonaje);
            Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
            index++;
        }

        if (rtsInstanciado != null)
        {
            Destroy(rtsInstanciado);
        }

        if (nodos.Count > 0)
        {
            personajeObject.transform.position = new Vector3(nodos[index - 1].X / 2f, 0f, nodos[index - 1].Y / 2f);
        }

        nodos.Clear();

        if (!hayNuevoObjetivo)
        {
            animador.Play((byte)Tipos.Animaciones.IDLE);
            movimiento = null;

            int idPortal = aEstrella.Mapa[(int)posicionActual.x, (int)posicionActual.y].IdPortal;

            if (idPortal != -1)
            {
                Portal portal = gestorPortales.ObtenerPortal(idPortal);

                if(portal != null)
                {
                    NuevoMapaX = portal.DestinoX;
                    NuevoMapaY = portal.DestinoY;
                    UltimoMapa = Personaje.EntidadCombate.Mapa;
                    Personaje.EntidadCombate.Mapa = portal.MapaDestino;
                    aEstrella = GestorMapas.Instancia.AEstrellasMapas[portal.MapaDestino];
                    GestorDeEventos.CambiarEscena.Invoke("Mapa_" + portal.MapaDestino.ToString());
                    IU.CerrarPanelTienda();
                    IU.CerrarChat();
                }              
            }

            if(hayObjetivo)
            {
                CogerObjeto();              
            }
        }
        else
        {
            StopCoroutine(movimiento);
            GenerarRuta(false);
        }
    }

    private void CogerObjeto()
    {
        MS_CogerItem ms = new(IdItemSuelo);
        Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
    }

    public void CerrarPersonaje()
    {
        Emisor.Enviar((byte)Tipos.MensajeSaliente.MS_CERRAR_PERSONAJE, "");
        GestorDeEventos.CambiarEscena.Invoke("Personajes");
    }

    private void EscenaCargada(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == "Personajes" || escena.name == "Login")
        {
            GestorEscena.Instancia.RestablecerGestores();
            Instancia = null;
            Destroy(gameObject);
        }   
        else if(escena.name.StartsWith("Mapa"))
        {
            if(NuevoMapaX != -1 && NuevoMapaY != -1)
            {
                PosicionarEnNuevoMapa();
                gestorPortales = FindObjectOfType<GestorPortales>();
            }           
        }
    }

    public int ObtenerDireccion(Vector2 origin, Vector2 destination)
    {
        Vector2 direction = new(destination.y - origin.y, destination.x - origin.x);
        int directionIndex = Mathf.RoundToInt(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg / 45f);
        if (directionIndex < 0)
        {
            directionIndex += 8;
        }
        return directionIndex + 1;
    }

    public float ObtenerAnguloDeDireccion(int direction)
    {
        return (direction - 1) * 45f;
    }

    public Vector2 ObtenerCoordenadas(Vector3 point)
    {
        if (point != null)
        {
            double x = System.Math.Round(point.x, 2);
            double z = System.Math.Round(point.z, 2);

            double mod_x = x % 0.5f;
            double mod_z = z % 0.5f;

            int final_x = (int)System.Math.Truncate(x / 0.5f);
            int final_z = (int)System.Math.Truncate(z / 0.5f);

            if (mod_x >= 0.25) { final_x++; }
            if (mod_z >= 0.25) { final_z++; }

            return new Vector2(final_x, final_z);
        }
        return new Vector2(-1, -1);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= EscenaCargada;
    }
}