using Assets.Scripts.Game.Modelos.ItemsTienda;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Modelos.Tiendas
{
    public class Tienda
    {
        public int IdTienda { get; set; }
        public string NombreTienda { get; set; }
        public byte RatioCompra { get; set; }
        public string NombreNpc { get; set; }
        public byte OrientacionNpc { get; set; }
        public short TiendaX { get; set; }
        public short TiendaY { get; set; }
        public short MapaTienda { get; set; }

        public List<ItemTienda> ItemsTienda { get; set; }
    }
}