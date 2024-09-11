namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    internal class MS_CambioMapa : IMensajeSaliente
    {
        public long IdPersonaje { get; set; }
        public short UltimoMapa { get; set; }
        public short NuevoMapa { get; set; }
        public short X { get; set; }
        public short Y { get; set; }

        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_CAMBIO_MAPA; }
        public MS_CambioMapa(long idPersonaje, short ultimoMapa, short nuevoMapa, short x, short y)
        {
            IdPersonaje = idPersonaje;
            UltimoMapa = ultimoMapa;
            NuevoMapa = nuevoMapa;
            X = x;
            Y = y;
        }
    }
}