using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorInventario : MonoBehaviour
{
    public GameObject ItemsConsumo;
    public GameObject ItemsEquipo;
    public GameObject Maestrias;
    public GameObject Miscelanea;

    public Image BotonConsumo;
    public Image BotonEquipo;
    public Image BotonMaestrias;
    public Image BotonMiscelanea;

    public TextMeshProUGUI Monedas;

    public List<GameObject> RanurasItemsConsumo;
    public List<GameObject> RanurasItemsEquipo;
    public List<GameObject> RanurasMaestrias;
    public List<GameObject> RanurasMiscelanea;

    public List<ItemBase> Items;

    private Image panelImage;

    public void Awake()
    {
        panelImage = GetComponent<Image>();

        GestorDeEventos.MonedasActuales += EstablecerMonedas;
        GestorDeEventos.NuevoItemInventario += AgregarItem;
        GestorDeEventos.AumentoCantidadItemInventario += AumentarCantidadItemInventario;
    }

    public void CentrarConTienda()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(600f, 0f); ;
    }

    public void VisibilidadInventario()
    {
        if (panelImage != null && panelImage.color.a == 0)
        {
            MostrarInventario();
        }
        else
        {
            OcultarInventario();
        }
    }

    public void EstablecerMonedas(long monedas)
    {
        Monedas.text = monedas.ToString("N0").Replace(",", ".");
    }

    public void OcultarInventario()
    {
        CambiarVisibilidadHijos(gameObject, 0, false);
    }

    public void MostrarInventario()
    {
        CambiarVisibilidadHijos(gameObject, 1, true);
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

    public void AumentarCantidadItemInventario(byte seccionInventario, short cantidadAumentada, byte ranura)
    {
        switch (seccionInventario)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO: { AumentarItemConsumo(cantidadAumentada, ranura); } break;
            case (byte)Tipos.SeccionesInventario.MISCELANEA: { AumentarItemMiscelanea(cantidadAumentada, ranura); } break;

            default: { } break;
        }
    }

    public void AgregarItem(byte seccionInventario, short refItem, short cantidad, byte ranura)
    {
        switch(seccionInventario)
        {
            case (byte)Tipos.SeccionesInventario.CONSUMO: { AgregarItemConsumo(refItem, cantidad, ranura); }break;
            case (byte)Tipos.SeccionesInventario.EQUIPO: { AgregarItemEquipo(refItem, cantidad, ranura); } break;
            case (byte)Tipos.SeccionesInventario.MAESTRIA: { AgregarMaestria(refItem, cantidad, ranura); } break;
            case (byte)Tipos.SeccionesInventario.MISCELANEA: { AgregarMiscelanea(refItem, cantidad, ranura); } break;

            default: { } break;
        }
    }

    public void AgregarItemConsumo(short refItem, short cantidad, byte ranura)
    {
        bool oculto = panelImage != null && panelImage.color.a == 0;

        RanurasItemsConsumo[ranura].GetComponent<Ranura>().InstanciarItem(Items[(int)refItem - 1], cantidad, oculto);
        GestorPersonaje.Instancia.AgregarItemInventario((byte)Tipos.SeccionesInventario.CONSUMO, refItem, cantidad, ranura);
    }

    public void AgregarItemEquipo(short refItem, short cantidad, byte ranura)
    {
        RanurasItemsEquipo[ranura].GetComponent<Ranura>().InstanciarItem(Items[(int)refItem - 1], cantidad, false);
        GestorPersonaje.Instancia.AgregarItemInventario((byte)Tipos.SeccionesInventario.EQUIPO, refItem, cantidad, ranura);
    }

    public void AgregarMaestria(short refItem, short cantidad, byte ranura)
    {
        RanurasMaestrias[ranura].GetComponent<Ranura>().InstanciarItem(Items[(int)refItem - 1], cantidad, false);
        GestorPersonaje.Instancia.AgregarItemInventario((byte)Tipos.SeccionesInventario.MAESTRIA, refItem, cantidad, ranura);
    }

    public void AgregarMiscelanea(short refItem, short cantidad, byte ranura)
    {
        bool oculto = panelImage != null && panelImage.color.a == 0;

        RanurasMiscelanea[ranura].GetComponent<Ranura>().InstanciarItem(Items[(int)refItem - 1], cantidad, oculto);
        GestorPersonaje.Instancia.AgregarItemInventario((byte)Tipos.SeccionesInventario.MISCELANEA, refItem, cantidad, ranura);
    }

    public void AumentarItemConsumo(short cantidadAumentada, byte ranura)
    {
        RanurasItemsConsumo[ranura].GetComponent<Ranura>().AumentarCantidadItem(cantidadAumentada);
        GestorPersonaje.Instancia.AumentarItemInventario((byte)Tipos.SeccionesInventario.CONSUMO, cantidadAumentada, ranura);
    }

    public void AumentarItemMiscelanea(short cantidadAumentada, byte ranura)
    {
        RanurasMiscelanea[ranura].GetComponent<Ranura>().AumentarCantidadItem(cantidadAumentada);
        GestorPersonaje.Instancia.AumentarItemInventario((byte)Tipos.SeccionesInventario.MISCELANEA, cantidadAumentada, ranura);
    }

    public void Habilitar()
    {
        ActivarItemsConsumo();
        OcultarInventario();
    }

    public void ActivarItemsConsumo()
    {
        BotonConsumo.color = Color.black;
        BotonEquipo.color = Color.white;
        BotonMaestrias.color = Color.white;
        BotonMiscelanea.color = Color.white;

        ItemsConsumo.transform.SetAsLastSibling();
    }

    public void ActivarItemsEquipo()
    {
        BotonConsumo.color = Color.white;
        BotonEquipo.color = Color.black;
        BotonMaestrias.color = Color.white;
        BotonMiscelanea.color = Color.white;

        ItemsEquipo.transform.SetAsLastSibling();
    }

    public void ActivarMaestrias()
    {
        BotonConsumo.color = Color.white;
        BotonEquipo.color = Color.white;
        BotonMaestrias.color = Color.black;
        BotonMiscelanea.color = Color.white;

        Maestrias.transform.SetAsLastSibling();
    }

    public void ActivarMiscelanea()
    {
        BotonConsumo.color = Color.white;
        BotonEquipo.color = Color.white;
        BotonMaestrias.color = Color.white;
        BotonMiscelanea.color = Color.black;

        Miscelanea.transform.SetAsLastSibling();
    }

    private void OnDestroy()
    {
        GestorDeEventos.MonedasActuales -= EstablecerMonedas;
        GestorDeEventos.NuevoItemInventario -= AgregarItem;
        GestorDeEventos.AumentoCantidadItemInventario -= AumentarCantidadItemInventario;
    }
}