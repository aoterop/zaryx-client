using Assets.Scripts.Game.Modelos.Tiendas;
using TMPro;
using UnityEngine;

public class GestorTienda : MonoBehaviour
{
    public TextMeshPro nombreNpc;
    public TextMeshProUGUI tituloTienda;

    private Tienda tienda;
    public void Inicializar(Tienda t)
    {
        tienda = t;
        nombreNpc.text = tienda.NombreNpc;
        tituloTienda.text = tienda.NombreTienda;
    }

    public void AbrirPanelTienda()
    {
        GestorIU.Instancia.AbrirPanelTienda(tienda);
    }
}