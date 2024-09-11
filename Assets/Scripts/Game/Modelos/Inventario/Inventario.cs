using System.Collections.Generic;

public class Inventario
{
    public Dictionary<byte, ItemPersonaje> ItemsConsumo { get; set; } = new Dictionary<byte, ItemPersonaje>();
    public Dictionary<byte, ItemPersonaje> ItemsEquipo { get; set; } = new Dictionary<byte, ItemPersonaje>();
    public Dictionary<byte, ItemPersonaje> Maestrias { get; set; } = new Dictionary<byte, ItemPersonaje>();
    public Dictionary<byte, ItemPersonaje> Miscelanea { get; set; } = new Dictionary<byte, ItemPersonaje>();
}