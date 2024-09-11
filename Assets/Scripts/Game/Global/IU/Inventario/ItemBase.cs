using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Items/ItemBase")]
public class ItemBase : ScriptableObject
{
    public short IdItem;
    public string NombreItem;
    public string DetallesItem;
    public long Precio;
    public bool EsArrojable;
    public Tipos.SeccionesInventario SeccionInventario;
    public Tipos.Items TipoItem;
    public Sprite Icono;
}