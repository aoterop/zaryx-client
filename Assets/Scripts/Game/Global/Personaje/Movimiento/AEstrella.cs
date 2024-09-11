using System.Collections.Generic;
using System;
using System.Linq;

public class AEstrella
{
    public static int NumFilas { get; private set; }
    public static int NumColumnas { get; private set; }
    public Nodo[,] Mapa { get; set; }

    public AEstrella(Nodo[,] mapa, int filas, int columnas)
    {
        Mapa = mapa;
        NumFilas = filas;
        NumColumnas = columnas;
    }

    public List<Nodo> EncontrarRuta(Nodo nodoPartida, Nodo nodoFinal)
    {
        List<Nodo> nodosAbiertos = new(); // Lista de nodos por evaluar.
        List<Nodo> nodosCerrados = new(); // Lista de nodos evaluados.

        nodoPartida.G = 0;
        nodoPartida.H = Heuristica(nodoPartida, nodoFinal);
        nodosAbiertos.Add(nodoPartida);

        while (nodosAbiertos.Count > 0)
        {
            Nodo current = nodosAbiertos[0];
            for (int i = 1; i < nodosAbiertos.Count; i++)
            {
                if (nodosAbiertos[i].F() < current.F() || nodosAbiertos[i].F() == current.F() && nodosAbiertos[i].H < current.H)
                {
                    current = nodosAbiertos[i];
                }
            }

            nodosAbiertos.Remove(current);
            nodosCerrados.Add(current);

            // Calcular los vecinos del nodo.
            CalcularVecinos(current, Mapa);

            if (current == nodoFinal)
            {
                List<Nodo> path = ReconstruirCamino(nodoPartida, nodoFinal);
                ResetearNodos(nodosAbiertos, nodosCerrados);
                nodoPartida.Reset();
                nodoFinal.Reset();
                return path;
            }

            foreach (Nodo sucesor in current.Sucesores)
            {
                if (nodosCerrados.Contains(sucesor))
                {
                    continue;
                }

                float tentativeG = current.G + DistanciaAlSucesor(current, sucesor);

                if (!nodosAbiertos.Contains(sucesor))
                {
                    nodosAbiertos.Add(sucesor);
                }
                else if (tentativeG >= sucesor.G)
                {
                    continue;
                }

                sucesor.G = tentativeG;
                sucesor.H = Heuristica(sucesor, nodoFinal);
            }
        }
        return null; // No se encontró ninguna solución.
    }


    private static void ResetearNodos(List<Nodo> nodosAbiertos, List<Nodo> nodosCerrados)
    {
        foreach (Nodo node in nodosAbiertos.Concat(nodosCerrados))
        {
            node.Reset();
        }
    }

    private static List<Nodo> ReconstruirCamino(Nodo nodoPartida, Nodo nodoFinal)
    {
        List<Nodo> path = new();
        Nodo current = nodoFinal;

        while (current != nodoPartida)
        {
            path.Add(current);
            if (current.Sucesores.Count > 0)
            {
                current = SucesorDeMenorCoste(current);
            }
        }

        path.Add(nodoPartida);
        path.Reverse();

        return path;
    }

    private static void CalcularVecinos(Nodo a, Nodo[,] graph)
    {
        a.Sucesores.Clear();

        if (EnRango(a.X + 1, a.Y))
        {
            if (graph[a.X + 1, a.Y].EsCaminable) { a.Sucesores.Add(graph[a.X + 1, a.Y]); }
        }

        if (EnRango(a.X + 1, a.Y - 1))
        {
            if (graph[a.X + 1, a.Y - 1].EsCaminable) { a.Sucesores.Add(graph[a.X + 1, a.Y - 1]); }
        }

        if (EnRango(a.X + 1, a.Y + 1))
        {
            if (graph[a.X + 1, a.Y + 1].EsCaminable) { a.Sucesores.Add(graph[a.X + 1, a.Y + 1]); }
        }

        if (EnRango(a.X, a.Y + 1))
        {
            if (graph[a.X, a.Y + 1].EsCaminable) { a.Sucesores.Add(graph[a.X, a.Y + 1]); }
        }

        if (EnRango(a.X, a.Y - 1))
        {
            if (graph[a.X, a.Y - 1].EsCaminable) { a.Sucesores.Add(graph[a.X, a.Y - 1]); }
        }

        if (EnRango(a.X - 1, a.Y))
        {
            if (graph[a.X - 1, a.Y].EsCaminable) { a.Sucesores.Add(graph[a.X - 1, a.Y]); }
        }

        if (EnRango(a.X - 1, a.Y + 1))
        {
            if (graph[a.X - 1, a.Y + 1].EsCaminable) { a.Sucesores.Add(graph[a.X - 1, a.Y + 1]); }
        }

        if (EnRango(a.X - 1, a.Y - 1))
        {
            if (graph[a.X - 1, a.Y - 1].EsCaminable) { a.Sucesores.Add(graph[a.X - 1, a.Y - 1]); }
        }
    }

    private static bool EnRango(int fila, int columna)
    {
        if (fila >= 0 && fila < NumFilas && columna >= 0 && columna < NumColumnas)
        {
            return true;
        }
        else { return false; }
    }

    private static int Heuristica(Nodo from, Nodo to)
    {
        return Distancia(from, to);
    }

    private static int Distancia(Nodo from, Nodo to)
    {
        return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
    }

    private static float DistanciaAlSucesor(Nodo from, Nodo to)
    {
        if (Distancia(from, to) == 1)
        {
            return 1;
        }
        else { return 1.4f; }
    }

    private static Nodo SucesorDeMenorCoste(Nodo node)
    {
        return node.Sucesores.Where(n => n.H > 0 && n.Sucesores.Count > 0).OrderBy(n => n.G).FirstOrDefault();
    }
}