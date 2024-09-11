using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemRanura : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Transform Inventario;

    private RawImage image;
    public ItemBase itemData;
    public int Cantidad = 0;
    private TextMeshProUGUI TextoCantidad;

    public void Awake()
    {
        image = GetComponent<RawImage>();
        TextoCantidad = GetComponentInChildren<TextMeshProUGUI>();
        TextoCantidad.text = "";
        Inventario = GameObject.Find("Inventario").transform;
    }


    public void Inicializar(ItemBase data, int cantidad)
    {
        itemData = data;
        image.texture = itemData.Icono.texture;

        if(Inventario.gameObject.GetComponent<Image>().color.a == 0f)
        {
            Color color = image.color;
            color.a = 0;
            image.color = color;
            image.raycastTarget = false;
        }

        if(data.SeccionInventario == Tipos.SeccionesInventario.MAESTRIA || 
           data.SeccionInventario == Tipos.SeccionesInventario.EQUIPO)
        {
            Cantidad = 1;
            TextoCantidad.text = "";
        }
        else
        {
            Cantidad = cantidad;
            TextoCantidad.text = cantidad.ToString();
        }
    }

    public void IncrementarCantidad(int aumento)
    {
        Cantidad += aumento;
        TextoCantidad.text = Cantidad.ToString();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(Inventario);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // Realiza un raycast en la posición del ratón
            List<RaycastResult> raycastResults = new();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            
            if(raycastResults.Count > 0 )
            {
                // Encuentra el primer objeto Ranura en los resultados del raycast
                Ranura ranura = raycastResults.Select(result => result.gameObject.GetComponent<Ranura>()).FirstOrDefault(r => r != null);

                if (ranura != null)
                { // Si se ha encontrado una Ranura...                          

                    try
                    {
                        byte ranura1 = byte.Parse(parentAfterDrag.gameObject.name);
                        byte ranura2 = byte.Parse(ranura.gameObject.transform.name);

                        if (ranura.item == null)
                        {// Movimiento básico.
                            var a = parentAfterDrag.GetComponent<Ranura>();
                            transform.SetParent(ranura.transform);
                            ranura.PosicionarItem(this);
                            a.EliminarItem();

                            GestorPersonaje.Instancia.MovimientoSlotBasico((byte)itemData.SeccionInventario, ranura1, ranura2);

                            MS_IntercambioSlot ms = new((byte)itemData.SeccionInventario, ranura1, ranura2);
                            Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                        }
                        else
                        {
                            if (ranura.gameObject.transform.name != parentAfterDrag.gameObject.name)
                            {// Estamos en una ranura diferente a la de partida.
                             // Hacer el intercambio.
                                if ((itemData.SeccionInventario == Tipos.SeccionesInventario.MAESTRIA || itemData.SeccionInventario == Tipos.SeccionesInventario.EQUIPO) || itemData.IdItem != ranura.item.itemData.IdItem)
                                {// Son items de maestria o equipo o bien, los items son diferentes: rotación clásica.
                                    var a = parentAfterDrag.GetComponent<Ranura>(); // Ranura de la que partimos.
                                    transform.SetParent(ranura.transform); // Ponemos este item como hijo de la ranura destino.
                                    var itemB = ranura.item;
                                    ranura.PosicionarItem(this); // En la ranura destino le asignamos a nivel lógico este item.
                                    a.EliminarItem(); // Quitamos de la ranura original el item a nivel lógico.
                                    a.PosicionarItem(itemB); // Ponemos en la ranura original el item de la otra ranura a nivel lógico.
                                    ranura.transform.GetChild(0).SetParent(parentAfterDrag); // Ponemos el otro item como hijo de la ranura de la que partimos.

                                    GestorPersonaje.Instancia.RotationClasica((byte)itemData.SeccionInventario, ranura1, ranura2);
                                }
                                else if (itemData.IdItem == ranura.item.itemData.IdItem)
                                {// Los objetos son del mismo tipo (fusión).
                                    var a = parentAfterDrag.GetComponent<Ranura>(); // Ranura de la que partimos.
                                    ranura.item.IncrementarCantidad(Cantidad); // En la ranura destino le incrementamos la cantidad.
                                    a.EliminarItem(); // Eliminamos a nivel lógico el item de la ranura origen.
                                    Destroy(gameObject); // Destruimos el gameObject.

                                    GestorPersonaje.Instancia.FusionSlots((byte)itemData.SeccionInventario, ranura1, ranura2);
                                }

                                MS_IntercambioSlot ms = new((byte)itemData.SeccionInventario, ranura1, ranura2);
                                Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                            }
                            else { transform.SetParent(parentAfterDrag); }
                        }
                    }
                    catch { transform.SetParent(parentAfterDrag); }
                }
                else
                {// Si no se ha encontrado una Ranura, vuelve al padre original
                    if (raycastResults.LastOrDefault().gameObject.transform.parent.parent.name == "Inventario")
                    {// El item va a ser arrojado fuera del inventario.
                        if (itemData.EsArrojable)
                        {// Se arroja el item.
                            MS_ArrojarItem ms = new((byte)itemData.SeccionInventario, byte.Parse(parentAfterDrag.gameObject.name));
                            Emisor.Enviar(ms.Tipo(), Serializador.Serializar(ms));
                            var a = parentAfterDrag.GetComponent<Ranura>(); // Ranura de la que partimos.
                            a.EliminarItem();
                            Destroy(gameObject); // Destruimos el gameObject.

                            GestorPersonaje.Instancia.EliminarItemInventario((byte)itemData.SeccionInventario, byte.Parse(parentAfterDrag.gameObject.name));
                        }
                        else { transform.SetParent(parentAfterDrag); }
                    }
                    else { transform.SetParent(parentAfterDrag); }
                }
            }
            else { transform.SetParent(parentAfterDrag); }
            image.raycastTarget = true;
        }       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            GestorIU.Instancia.MostrarInformacionItem(itemData);
        }
    }
}