public static class Tipos
{
    public enum ComponentePersonaje
    {
        CINTO,
        ARMADURA,
        CARA,
        GUANTE,
        PEINADO,
        CASCO,
        MOCHILA,
        ARMAMENTO_IZQ,
        ARMAMENTO_DCHA,
        BOTA,
        HOMBRERA      
    }

    public enum AspectoFacial
    {
        AspectoFacial1 = 1,
        AspectoFacial2,
        AspectoFacial3,
        AspectoFacial4,
        AspectoFacial5
    }

    public enum EstiloPeinado
    {
        Peinado1 = 1,
        Peinado2,
        Peinado3,
        Peinado4,
        Peinado5,
        Peinado6,
        Peinado7
    }

    public enum Clase : byte
    {
        GUERRERO = 0,
        TIRADOR = 1,
        MAGO = 2
    }

    public enum MensajeSaliente
    {
        MS_LOGIN = 0,
        MS_CREAR_PERSONAJE = 1,
        MS_SOLICITAR_PERSONAJES = 2,
        MS_BORRAR_PERSONAJE = 3,
        MS_PERSONAJE_ESCOGIDO = 4,
        MS_CAMBIO_MAPA = 5,
        MS_MOVIMIENTO_PERSONAJE = 6,
        MS_ACTUALIZAR_POSICION = 7,
        MS_INTERCAMBIO_SLOTS = 8,
        MS_ARROJAR_ITEM = 9,
        MS_COGER_ITEM = 10,
        MS_COMPRA_ITEM_TIENDA = 11,
        MS_VENTA_ITEM_TIENDA = 12,
        MS_MENSAJE_CHAT = 13,

        MS_CERRAR_PERSONAJE = 253,
        MS_LOG_OUT = 254,
        MS_CIERRE_SESION = 255
    }

    public enum MensajeEntrante
    {
        ME_LOGIN = 0,
        ME_CREAR_PERSONAJE = 1,
        ME_RECIBIR_PERSONAJES = 2,
        ME_PERSONAJE_BORRADO = 3,
        ME_NUEVO_PERSONAJE_MAPA = 4,
        ME_PERSONAJES_MAPA = 5,
        ME_SALIDA_PERSONAJE = 6,
        ME_MOVIMIENTO_PERSONAJE = 7,
        ME_PORTALES_MAPA = 8,
        ME_INSTANCIA_ITEM_MAPA = 9,
        ME_ELIMINAR_ITEM_MAPA = 10,
        ME_NUEVO_ITEM_INVENTARIO = 11,
        ME_AUMENTO_CANTIDAD_ITEM_INVENTARIO = 12,
        ME_TIENDAS_MAPA = 13,
        ME_MONEDAS_ACTUALES = 14,
        ME_NUEVO_MENSAJE_CHAT = 15,

    }

    public enum EstadoCuenta : byte
    {
        NO_EXISTE,
        CREDENCIALES_INCORRECTAS,
        CREDENCIALES_CORRECTAS,
        INACTIVA,
        BANEADA,
        EN_USO
    }

    public enum Animaciones : byte
    {
        IDLE,
        MOVIMIENTO,
        ATAQUE
    }

    public enum Items : byte
    {
        CONSUMO,
        EQUIPO_DEFENSIVO,
        EQUIPO_OFENSIVO,
        MAESTRIA_GUERRERO,
        MAESTRIA_TIRADOR,
        MISCELANEA,
        NO_ESPECIFICADO = 255
    }

    public enum SeccionesInventario : byte
    {
        CONSUMO,
        EQUIPO,
        MAESTRIA,
        MISCELANEA
    }

    public enum MensajeChat : byte
    {
        NORMAL,
        GLOBAL,
        PRIVADO,
        GRUPAL
    }
}