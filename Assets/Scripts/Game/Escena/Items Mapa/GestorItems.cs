using System;
using System.Collections.Generic;
using UnityEngine;

public class GestorItems : MonoBehaviour
{
    public GameObject ItemSuelo;
    public Dictionary<long, GameObject> ItemsSuelo;

    public Transform Canvas;
    public List<ItemBase> Items;
    private object locker = new object();
    private Quaternion rotacionInicial = Quaternion.Euler(37.0f, 0f, 0f);


    private void Awake()
    {
        ItemsSuelo = new Dictionary<long, GameObject>();
        GestorDeEventos.InstanciarItemMapa += AgregarItemAlSuelo;
        GestorDeEventos.EliminarItemMapa += EliminarItemDelSuelo;
        GestorEscena.Instancia.RegistrarGestor();
    }


    public void AgregarItemAlSuelo(long idItemSuelo, short referenciaItem, short cantidad, short x, short y)
    {
        lock(locker)
        {
            if (!ItemsSuelo.ContainsKey(idItemSuelo))
            {
                GameObject gO = Instantiate(ItemSuelo, new Vector3(x / 2f, 0f, y / 2f), rotacionInicial, Canvas);
                gO.GetComponent<GestorItem>().Inicializar(Items[referenciaItem - 1], cantidad, idItemSuelo, x, y);

                ItemsSuelo.Add(idItemSuelo, gO);
            }
        }     
    }

    public void EliminarItemDelSuelo(long idItemSuelo)
    {
        lock(locker)
        {
            if (ItemsSuelo.ContainsKey(idItemSuelo))
            {
                Destroy(ItemsSuelo[idItemSuelo]);
                ItemsSuelo.Remove(idItemSuelo);
            }
        }
    }

    private void OnDestroy()
    {
        GestorDeEventos.InstanciarItemMapa -= AgregarItemAlSuelo;
        GestorDeEventos.EliminarItemMapa -= EliminarItemDelSuelo;
    }
}