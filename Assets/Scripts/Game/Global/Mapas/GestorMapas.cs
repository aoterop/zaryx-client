using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GestorMapas : MonoBehaviour
{
    public static GestorMapas Instancia { get; private set; }
    public Dictionary<short, AEstrella> AEstrellasMapas { get; set; }
    public List<TextAsset> archivos;
    public Dictionary<short, string> NombresMapas { get; set; }

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            AEstrellasMapas = new Dictionary<short, AEstrella>();
            NombresMapas = new Dictionary<short, string>();
            StartCoroutine(CargarMapas());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CargarMapas()
    {// Cargar todos los TextAssets en la carpeta especificada

        var regex = new Regex(@"\((\d+),\s*(\d+)\)");
        byte mapasCargados = 0;
                
        foreach (var archivo in archivos)
        {
            // Leer cada línea del archivo
            string[] lines = archivo.text.Split('\n');

            if(lines.Length > 0 )
            {
                string nombreMapa = lines[0];
                string[] dimensiones = lines[1].Split("x");                

                if (dimensiones.Length == 2)
                {
                    Nodo[,] nodos = new Nodo[int.Parse(dimensiones[0]), int.Parse(dimensiones[1])];

                    for (short i = 0; i < short.Parse(dimensiones[0]); i++)
                    {
                        for (short j = 0; j < short.Parse(dimensiones[1]); j++)
                        {
                            nodos[i, j] = new Nodo(i, j);
                        }
                        yield return null;
                    }

                    foreach (var line in lines)
                    {
                        var match = regex.Match(line);
                        if (match.Success)
                        {
                            var x = int.Parse(match.Groups[1].Value);
                            var y = int.Parse(match.Groups[2].Value);

                            nodos[x, y].EsCaminable = false;
                        }                       
                    }

                    NombresMapas.Add(short.Parse(archivo.name.ToString()), nombreMapa);


                    AEstrellasMapas.Add(short.Parse(archivo.name.ToString()), new AEstrella(nodos, short.Parse(dimensiones[0]), short.Parse(dimensiones[1])));
                    mapasCargados++;
                }
            }
            yield return null;
        }
    }
}