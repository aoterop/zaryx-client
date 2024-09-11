using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorItemTienda : MonoBehaviour
{
    public TextMeshProUGUI NombreItem;
    public TextMeshProUGUI Precio;
    public RawImage Icono;

    private ItemBase itemData;

    public void Inicializar(ItemBase item)
    {
        itemData = item;

        NombreItem.text = item.NombreItem;
        Precio.text = item.Precio.ToString();
        Icono.texture = item.Icono.texture;
    }

    public void ComprarItem()
    {
        MS_CompraItemTienda ms = new(itemData.IdItem, 1);
        Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
    }
}