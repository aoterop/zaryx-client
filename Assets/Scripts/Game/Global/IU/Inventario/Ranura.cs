using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ranura : MonoBehaviour
{
    public ItemRanura item; // El hijo de item si hay uno.
    public GameObject ItemRanura;

    public void InstanciarItem(ItemBase itemData, int cantidad, bool oculto)
    {
        if(item == null)
        {
            item = Instantiate(ItemRanura, transform).GetComponent<ItemRanura>();
            item.Inicializar(itemData, cantidad);
            
            if(oculto)
            {
                CambiarVisibilidadHijos(item.gameObject, 0f, false);
            }
        }
    }

    public void AumentarCantidadItem(int cantidadAumentada)
    {
        if(item != null)
        {
            item.IncrementarCantidad(cantidadAumentada);
        }
    }

    public void PosicionarItem(ItemRanura itemRanura)
    {
        item = itemRanura;
    }

    public void CambiarVisibilidadHijos(GameObject parentObject, float alpha, bool raycast)
    {
        Image[] imagenes = parentObject.GetComponentsInChildren<Image>(true);
        foreach (Image imagen in imagenes)
        {
            Color color = imagen.color;
            color.a = alpha;
            imagen.color = color;
            imagen.raycastTarget = raycast;
        }

        RawImage[] imagenesRaw = parentObject.GetComponentsInChildren<RawImage>(true);
        foreach (RawImage imagenRaw in imagenesRaw)
        {
            Color color = imagenRaw.color;
            color.a = alpha;
            imagenRaw.color = color;
            imagenRaw.raycastTarget = raycast;
        }

        TextMeshProUGUI[] textos = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI texto in textos)
        {
            Color color = texto.color;
            color.a = alpha;
            texto.color = color;
            texto.raycastTarget = raycast;
        }
    }

    public void EliminarItem()
    {
        item = null;
    }
}