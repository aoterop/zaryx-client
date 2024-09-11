namespace Assets.Scripts.Red.Mensajeria.Mensajes.Salientes
{
    public class MS_ActualizarPosicion : IMensajeSaliente
    {
        public short X { get; set; }
        public short Y { get; set; }
        public long IdPersonaje { get; set; }

        public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_ACTUALIZAR_POSICION; }
        public MS_ActualizarPosicion(short x, short y, long idPersonaje)
        {
            X = x;
            Y = y;
            IdPersonaje = idPersonaje;
        }
    }
}