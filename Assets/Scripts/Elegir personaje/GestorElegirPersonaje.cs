using Assets.Scripts.Personajes;
using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GestorElegirPersonaje : MonoBehaviour
{
    private Tuple<byte, IPersonaje>[] personajesCuenta; // <ranura, personaje>>.
    private readonly object locker = new();
    private byte numPersonajes = 0;
    private byte ranuraSeleccionada = 255;
    private IPersonaje personaje; // Personaje para jugar.

    public GameObject selector;
    public GameObject panelBorrarPersonaje;   
    public List<GameObject> personajes;
    public List<GameObject> ranuras;
    public TextMeshProUGUI mensajeBorrarPersonaje;


    private void Awake()
    {
        personajesCuenta = new Tuple<byte, IPersonaje>[3];
        personajesCuenta[0] = null; // Primer Slot.
        personajesCuenta[1] = null; // Segundo Slot.
        personajesCuenta[2] = null; // Tercer Slot.

        MS_SolicitarPersonajes ms_personajes = new();
        Emisor.Enviar(ms_personajes.Tipo(), Serializador.Serializar(ms_personajes));

        GestorDeEventos.RellenarGuerrero += RellenarGuerrero;
        GestorDeEventos.RellenarTirador += RellenarTirador;
        GestorDeEventos.RellenarMago += RellenarMago;
        GestorDeEventos.BorrarPersonaje += BorrarPersonajeRanura;
    }

    public void CrearPersonaje()
    {
        if(numPersonajes <= 2)
        {
            GestorDeEventos.CambiarEscena.Invoke("CrearPersonaje");
        }
    }

    public Tuple<byte, IPersonaje> ObtenerPersonaje(byte pos)
    {
        return personajesCuenta[pos];
    }

    public void RellenarGuerrero(Guerrero guerrero)
    {
        lock (locker)
        {
            if (numPersonajes <= 3)
            {
                personajesCuenta[numPersonajes] = new Tuple<byte, IPersonaje>((byte)Tipos.Clase.GUERRERO, guerrero);
                RellenarRanura(numPersonajes, guerrero.EntidadCombate.Nombre, guerrero.EntidadCombate.Nivel.ToString(), Tipos.Clase.GUERRERO, (Tipos.AspectoFacial)guerrero.AspectoFacial, (Tipos.EstiloPeinado)guerrero.Peinado);
                numPersonajes++;
            }
        }
    }

    public void RellenarTirador(Tirador tirador)
    {
        lock (locker)
        {
            if (numPersonajes <= 3)
            {
                personajesCuenta[numPersonajes] = new Tuple<byte, IPersonaje>((byte)Tipos.Clase.TIRADOR, tirador);
                RellenarRanura(numPersonajes, tirador.EntidadCombate.Nombre, tirador.EntidadCombate.Nivel.ToString(), Tipos.Clase.TIRADOR, (Tipos.AspectoFacial)tirador.AspectoFacial, (Tipos.EstiloPeinado)tirador.Peinado);
                numPersonajes++;
            }
        }
    }

    public void RellenarMago(Mago mago)
    {/*
        lock (locker)
        {
            if (numPersonajes <= 3)
            {
                personajesCuenta[numPersonajes] = new Tuple<byte, IPersonaje>((byte)Tipos.Clase.MAGO, mago);
                RellenarRanura(numPersonajes, mago.EntidadCombate.Nombre, mago.EntidadCombate.Nivel.ToString());
                numPersonajes++;
            }
        }*/
    }

    public void SeleccionarPrimeraRanura()
    {
        if (personajesCuenta[0] != null)
        {
            personaje = personajesCuenta[0].Item2;
            selector.SetActive(true);
            selector.transform.position = new Vector3(26.5f, 0.01f, 3.5f);
            ranuraSeleccionada = 0;
        }
    }

    public void SeleccionarSegundaRanura()
    {
        if (personajesCuenta[1] != null)
        {
            personaje = personajesCuenta[1].Item2;
            selector.SetActive(true);
            selector.transform.position = new Vector3(30, 0.01f, 4);
            ranuraSeleccionada = 1;
        }
    }

    public void SeleccionarTerceraRanura()
    {    
        if (personajesCuenta[2] != null)
        {
            personaje = personajesCuenta[2].Item2;
            selector.SetActive(true);
            selector.transform.position = new Vector3(33.5f, 0.01f, 3.5f);
            ranuraSeleccionada = 2;
        }
    }

    public void CerrarSesion()
    {
        Emisor.Enviar((byte)Tipos.MensajeSaliente.MS_LOG_OUT, "");
        GestorDeEventos.CambiarEscena.Invoke("Login");
    }

    public void Jugar()
    {
        if(ranuraSeleccionada != 255)
        {// Hay un personaje seleccionado.

            if (personajesCuenta[ranuraSeleccionada] != null)
            {
                PersonajeSeleccionado.Instancia.Personaje = personajesCuenta[ranuraSeleccionada].Item2;
                PersonajeSeleccionado.Instancia.Clase = personajesCuenta[ranuraSeleccionada].Item1;

                try
                {
                    MS_PersonajeEscogido mensaje = new(personajesCuenta[ranuraSeleccionada].Item2.IdPersonaje, personajesCuenta[ranuraSeleccionada].Item2.Clase());
                    Emisor.Enviar(mensaje.Tipo(), Serializador.Serializar(mensaje));
                    GestorDeEventos.CambiarEscena.Invoke("Mapa_" + personaje.EntidadCombate.Mapa.ToString());
                }
                catch { }
            }
        }
    }

    public void MostrarPanelBorrarPersonaje()
    {
        if(ranuraSeleccionada != 255)
        {
            mensajeBorrarPersonaje.text = Alertas.Mensajes["BorrarPersonaje"];
            panelBorrarPersonaje.SetActive(true);
        }
    }

    public void BorrarPersonaje()
    {
        if(ranuraSeleccionada != 255)
        { 
            MS_BorrarPersonaje mensaje = new(personaje.IdPersonaje, personajesCuenta[ranuraSeleccionada].Item1);
            Emisor.Enviar(mensaje.Tipo(), Serializador.Serializar(mensaje));
            panelBorrarPersonaje.SetActive(false);
        }
    }

    public void BorrarPersonajeRanura(long idPersonaje)
    {
        lock(locker)
        {
            int pos = -1;

            for (int i = 0; i < personajesCuenta.Length; i++)
            {
                if (personajesCuenta[i] != null && personajesCuenta[i].Item2.IdPersonaje == idPersonaje)
                {
                    pos = i;
                }
            }

            if(pos != -1)
            {
                VaciarRanura((byte)pos);
                numPersonajes--;
                ranuraSeleccionada = 255;
            }
        }
    }

    private void VaciarRanura(byte ranura)
    {
        personajesCuenta[ranura] = null;
        selector.SetActive(false);
        personajes[ranura].SetActive(false);
        ranuras[ranura].transform.Find("Nombre").GetComponent<TextMeshProUGUI>().SetText("");
        ranuras[ranura].transform.Find("Nivel").GetComponent<TextMeshProUGUI>().SetText("");
    }

    public void RellenarRanura(byte posRanura, string nombre, string nivel, Tipos.Clase clase, Tipos.AspectoFacial aspectoFacial, Tipos.EstiloPeinado peinado)
    {
        ranuras[posRanura].transform.Find("Nombre").GetComponent<TextMeshProUGUI>().SetText(nombre);
        ranuras[posRanura].transform.Find("Nivel").GetComponent<TextMeshProUGUI>().SetText(nivel);

        RepresentarPersonaje(clase, posRanura, aspectoFacial, peinado);
    }

    public void RepresentarPersonaje(Tipos.Clase clase, byte posicion, Tipos.AspectoFacial aspectoFacial, Tipos.EstiloPeinado peinado)
    {
        switch(clase)
        {
            case Tipos.Clase.GUERRERO:
                {
                    personajes[posicion].GetComponent<GestorApariencia>().TransformarGuerreroSM();
                }
                break;

            case Tipos.Clase.TIRADOR:
                {
                    personajes[posicion].GetComponent<GestorApariencia>().TransformarTiradorSM();
                }
                break;

            case Tipos.Clase.MAGO:
                {
                    personajes[posicion].GetComponent<GestorApariencia>().TransformarMagoSM();
                }
                break;

            default: { } break;
        }
        personajes[posicion].GetComponent<GestorApariencia>().AplicarEstilos((byte)aspectoFacial, (byte)peinado);
        personajes[posicion].SetActive(true);
    }

    private void OnDestroy()
    {
        GestorDeEventos.RellenarGuerrero -= RellenarGuerrero;
        GestorDeEventos.RellenarTirador -= RellenarTirador;
        GestorDeEventos.RellenarMago -= RellenarMago;
        GestorDeEventos.BorrarPersonaje -= BorrarPersonajeRanura;
    }
}