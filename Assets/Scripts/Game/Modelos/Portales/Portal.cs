namespace Assets.Scripts.Game.Portales
{
    public class Portal
    {
        public int IdPortal { get; set; }
        public short DestinoX { get; set; }
        public short DestinoY { get; set; }
        public short OrigenX { get; set; }
        public short OrigenY { get; set; }
        public short MapaDestino { get; set; }
        public short MapaOrigen { get; set; }
        public byte AparienciaPortal { get; set; }
    }
}