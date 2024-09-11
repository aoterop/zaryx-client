using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorInfoItem : MonoBehaviour
{
    public RawImage Icono;
    public TextMeshProUGUI Nombre;
    public TextMeshProUGUI Precio;
    public GameObject Dato;
    public GameObject Contenido;

    public void RellenarDatos(ItemBase datosItem)
    {
        Icono.texture = datosItem.Icono.texture;
        Nombre.text = datosItem.NombreItem;
        Precio.text = "Precio: " + datosItem.Precio.ToString();

        ResetearContenido();

        GameObject descripcion = Instantiate(Dato, Contenido.transform);
        GameObject esArrojable = Instantiate(Dato, Contenido.transform);

        descripcion.GetComponent<TextMeshProUGUI>().text = datosItem.DetallesItem.ToString();

        if(datosItem.EsArrojable)
        {
            esArrojable.GetComponent<TextMeshProUGUI>().text = "Se puede arrojar";
            esArrojable.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        else
        {
            esArrojable.GetComponent<TextMeshProUGUI>().text = "No se puede arrojar";
            esArrojable.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        MostrarInfoItem();
    }


    public void ResetearContenido()
    {
        foreach (Transform child in Contenido.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void MostrarInfoItem()
    {
        CambiarVisibilidadHijos(gameObject, 1, true);
    }

    public void OcultarInfoItem()
    {
        CambiarVisibilidadHijos(gameObject, 0, false);
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
}