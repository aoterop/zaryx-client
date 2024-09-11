using System.Collections.Generic;

public class Nodo
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool EsCaminable { get; set; }
    public float G { get; set; } // Costo de la ruta desde el nodo inicial hasta este nodo.
    public float H { get; set; } // Heurística que estima el costo de la ruta desde este nodo hasta el nodo objetivo.
    public List<Nodo> Sucesores; // Lista de nodos sucesores.
    public int IdPortal { get; set; }

    public Nodo(int x, int y)
    {
        X = x;
        Y = y;
        G = 0;
        H = 0;
        EsCaminable = true;
        Sucesores = new List<Nodo>();
        IdPortal = -1;
    }

    public void Reset()
    {
        G = 0;
        H = 0;
        Sucesores.Clear();
    }

    public float F()
    {
        return G + H;
    }
}