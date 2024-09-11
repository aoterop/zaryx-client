using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorItem : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    public int Cantidad = 1;
    public short X;
    public short Y;
    public long IdItemSuelo;
    public ItemBase itemData;
    private RawImage image;
    private Animator animator;
    public TextMeshProUGUI nombre;
    
    public void Awake()
    {
        image = GetComponent<RawImage>();
        animator = GetComponent<Animator>();
    }

    public void Inicializar(ItemBase item, int cantidad, long idItemSuelo, short x, short y)
    {
        itemData = item;
        Cantidad = cantidad;
        IdItemSuelo = idItemSuelo;

        X = x;
        Y = y;
        image.texture = itemData.Icono.texture;
        nombre.text = item.NombreItem + " (x " + cantidad.ToString() + ")";
    }

    public Vector2 ObtenerCoordenadasItem()
    {
        return new Vector2(X, Y);
    }

    public void MouseEntra()
    {
        image.color = hoverColor;
    }

    public void MouseSale()
    {
        image.color = normalColor;
    }

    public void CogerItem()
    {
        //animator.Play("CogerItem");
    }
}