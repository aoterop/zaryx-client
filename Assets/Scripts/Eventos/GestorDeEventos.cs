using Assets.Scripts.Game.Modelos.Tiendas;
using Assets.Scripts.Game.Portales;
using Assets.Scripts.Personajes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeEventos : MonoBehaviour
{
    public static Action<string> CambiarEscena;
    public static Action ConectarConServidor;
    public static Action CredencialesIncorrectas;
    public static Action CuentaInexistente;
    public static Action NombrePersonajeEnUso;
    public static Action LoginExitoso;
    public static Action PersonajeCreado;
    public static Action CuentaEnUso;
    public static Action CuentaInactiva;
    public static Action CuentaBaneada;
    public static Action<Guerrero> RellenarGuerrero;
    public static Action<Tirador> RellenarTirador;
    public static Action<Mago> RellenarMago;
    public static Action<long> BorrarPersonaje;
    public static Action<IPersonaje, List<Nodo>> InstanciarNuevoPersonajeMapa;
    public static Action<long> EliminarPersonajeDeMapa;
    public static Action<long, List<Nodo>> MovimientoPersonaje;
    public static Action<Portal> InstanciarPortal;
    public static Action<long, short, short, short, short> InstanciarItemMapa;
    public static Action<long> EliminarItemMapa;
    public static Action<byte, short, short, byte> NuevoItemInventario;
    public static Action<byte, short, byte> AumentoCantidadItemInventario;
    public static Action<List<Tienda>> InstanciarTiendasMapa;
    public static Action<long> MonedasActuales;
    public static Action<MensajeChat> NuevoMensajeChat;
}