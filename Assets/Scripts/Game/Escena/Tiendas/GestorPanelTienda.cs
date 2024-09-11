using Assets.Scripts.Game.Modelos.Tiendas;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorPanelTienda : MonoBehaviour
{
    public GameObject Contenido;
    public GameObject Item;
    public TextMeshProUGUI NombreTienda;

    public List<ItemBase> Items;

    public RectTransform panelRectTransform;


    public void Habilitar()
    {
        OcultarPanelTienda();
    }

    void Centrar()
    {
        panelRectTransform.anchoredPosition = Vector2.zero;
    }

    public void AbrirTienda(Tienda t)
    {
        Centrar();
        ResetearTienda();
        RellenarConProductos(t);
        MostrarPanelTienda();
    }

    public void OcultarPanelTienda()
    {
        CambiarVisibilidadHijos(gameObject, 0, false);
        ResetearTienda();
    }

    public void MostrarPanelTienda()
    {
        CambiarVisibilidadHijos(gameObject, 1, true);
    }

    public void ResetearTienda()
    {
        foreach (Transform child in Contenido.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RellenarConProductos(Tienda tienda)
    {
        NombreTienda.text = tienda.NombreTienda;

        foreach(var item in tienda.ItemsTienda)
        {
            GameObject gO = Instantiate(Item, Contenido.transform);
            gO.GetComponent<GestorItemTienda>().Inicializar(Items[item.ItemOfertado - 1]);
        }
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