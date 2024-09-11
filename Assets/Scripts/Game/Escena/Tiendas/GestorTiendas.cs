using Assets.Scripts.Game.Modelos.Tiendas;
using System.Collections.Generic;
using UnityEngine;

public class GestorTiendas : MonoBehaviour
{
    public Dictionary<int, GameObject> TiendasMapa;
    public GameObject Npc;

    private void Awake()
    {
        TiendasMapa = new Dictionary<int, GameObject>();
        GestorDeEventos.InstanciarTiendasMapa += InstanciarTiendas;
        GestorEscena.Instancia.RegistrarGestor();
    }


    public void InstanciarTiendas(List<Tienda> tiendas)
    {
        foreach(var tienda in tiendas)
        {
            GameObject gO = Instantiate(Npc, new Vector3(tienda.TiendaX / 2f, 0f, tienda.TiendaY / 2f), ObtenerAnguloRotacion(tienda.OrientacionNpc), transform);
            gO.GetComponent<GestorTienda>().Inicializar(tienda);
            TiendasMapa.Add(tienda.IdTienda, gO);
        }
    }

    public Quaternion ObtenerAnguloRotacion(byte orientacion)
    {
        float grados = orientacion * 45f;
        return Quaternion.Euler(0f, grados, 0f);
    }

    private void OnDestroy()
    {
        GestorDeEventos.InstanciarTiendasMapa -= InstanciarTiendas;
    }
}