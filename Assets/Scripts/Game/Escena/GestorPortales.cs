using System.Collections.Concurrent;
using System;
using UnityEngine;
using Assets.Scripts.Game.Portales;
using System.Collections.Generic;

public class GestorPortales : MonoBehaviour
{
    public ConcurrentDictionary<int, Tuple<Portal, GameObject>> PortalesMapa;

    public List<GameObject> Portales;
   
    private void Awake()
    {
        PortalesMapa = new ConcurrentDictionary<int, Tuple<Portal, GameObject>>();
        GestorDeEventos.InstanciarPortal += InstanciarPortal;

        GestorEscena.Instancia.RegistrarGestor();
    }

    public void InstanciarPortal(Portal portal)
    {
        GameObject gO = Instantiate(Portales[portal.AparienciaPortal], new Vector3(portal.OrigenX / 2f, 0.01f, portal.OrigenY / 2f), Quaternion.identity, transform);

        PortalesMapa.TryAdd(portal.IdPortal, new Tuple<Portal, GameObject>(portal, gO));
    }

    public Portal ObtenerPortal(int idPortal)
    {
        PortalesMapa.TryGetValue(idPortal, out var tupla);

        if(tupla != null)
        {
            return tupla.Item1;
        }
        else
        {
            return null;
        }
    }

    public void OnDestroy()
    {
        GestorDeEventos.InstanciarPortal -= InstanciarPortal;
    }
}