using Assets.Scripts.Red.Mensajeria.Mensajes.Entrantes.Gestores;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorDeMensajes : MonoBehaviour
{
    public static ManejadorDeMensajes Instancia;

    private Dictionary<byte, IGestorMensaje> manejadorMensajes;

    void Awake()
    {
        if(ManejadorDeMensajes.Instancia == null)
        {
            ManejadorDeMensajes.Instancia = this;
            manejadorMensajes = new Dictionary<byte, IGestorMensaje>();
        }
    }

    internal void ManejarMensaje(byte tipoMensaje, string mensaje)
    {
        manejadorMensajes[tipoMensaje].GestionarMensaje(mensaje);
    }

    internal void RegistrarGestores()
    {
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_LOGIN, new GM_Login());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_CREAR_PERSONAJE, new GM_CrearPersonaje());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_RECIBIR_PERSONAJES, new GM_RecibirPersonajes());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_PERSONAJE_BORRADO, new GM_PersonajeBorrado());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_NUEVO_PERSONAJE_MAPA, new GM_EntradaNuevoPersonajeMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_PERSONAJES_MAPA, new GM_RecibirPersonajeMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_SALIDA_PERSONAJE, new GM_SalidaPersonaje());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_MOVIMIENTO_PERSONAJE, new GM_MovimientoPersonaje());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_PORTALES_MAPA, new GM_PortalesMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_INSTANCIA_ITEM_MAPA, new GM_InstanciaItemMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_ELIMINAR_ITEM_MAPA, new GM_EliminarItemMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_NUEVO_ITEM_INVENTARIO, new GM_NuevoItemInventario());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_AUMENTO_CANTIDAD_ITEM_INVENTARIO, new GM_AumentoCantidadItemInventario());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_TIENDAS_MAPA, new GM_RecibirTiendasMapa());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_MONEDAS_ACTUALES, new GM_MonedasActuales());
        manejadorMensajes.Add((byte)Tipos.MensajeEntrante.ME_NUEVO_MENSAJE_CHAT, new GM_NuevoMensajeChat());
        // Meter más...
    }
}