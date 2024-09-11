using Assets.Scripts.Red.Mensajeria.Mensajes.Salientes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GestorPersonajes : MonoBehaviour
{
    public ConcurrentDictionary<long, Tuple<IPersonaje, GameObject>> PersonajesMapa;
    public GameObject PersonajePrefab;
    public ConcurrentDictionary<long, Coroutine> CorutinasMovimientos;
    public ConcurrentDictionary<long, Nodo> NodosPersonajes;

    private void Awake()
    {
        PersonajesMapa = new ConcurrentDictionary<long, Tuple<IPersonaje, GameObject>>();
        CorutinasMovimientos = new ConcurrentDictionary<long, Coroutine>();
        NodosPersonajes = new ConcurrentDictionary<long, Nodo>();
        GestorDeEventos.EliminarPersonajeDeMapa += EliminarPersonaje;
        GestorDeEventos.InstanciarNuevoPersonajeMapa += AgregarPersonaje;
        GestorDeEventos.MovimientoPersonaje += MovimientoPersonaje;

        GestorEscena.Instancia.RegistrarGestor();
    }

    public void AgregarPersonaje(IPersonaje personaje, List<Nodo> nodos)
    {
        GameObject gO = Instantiate(PersonajePrefab, new Vector3(personaje.EntidadCombate.X / 2f, 0, personaje.EntidadCombate.Y / 2f), Quaternion.identity, transform);
        gO.SetActive(false);
        gO.name = "Personaje" + personaje.IdPersonaje.ToString();

        switch (personaje.Clase())
        {
            case (byte)Tipos.Clase.GUERRERO:
                {// EN EL FUTURO, METERLE AL MENSAJE UN CAMPO DE LA MAESTRÍA EQUIPADA para ver cual poner aquí....
                    gO.GetComponent<GestorApariencia>().TransformarGuerreroSM();
                }
                break;

            case (byte)Tipos.Clase.TIRADOR:
                {
                    gO.GetComponent<GestorApariencia>().TransformarTiradorSM();
                }
                break;

            case (byte)Tipos.Clase.MAGO:
                {
                    gO.GetComponent<GestorApariencia>().TransformarMagoSM();
                }
                break;

            default: { } break;
        }

        gO.GetComponent<GestorApariencia>().AplicarEstilos(personaje.AspectoFacial, personaje.Peinado);
        gO.GetComponent<GestorAnimaciones>().EstablecerAnimator(personaje.Clase());
        gO.GetComponentInChildren<GestorNombre>().EstablecerNombre(personaje.EntidadCombate.Nombre);
        gO.SetActive(true);
        PersonajesMapa.TryAdd(personaje.IdPersonaje, new Tuple<IPersonaje, GameObject>(personaje, gO));
        NodosPersonajes.TryAdd(personaje.IdPersonaje, null);
        CorutinasMovimientos.TryAdd(personaje.IdPersonaje, null);

        if (nodos != null && nodos.Count > 0)
        {
            MovimientoPersonaje(personaje.IdPersonaje, nodos);
        }
    }

    public void EliminarPersonaje(long idPersonaje)
    {
        if (PersonajesMapa.ContainsKey(idPersonaje))
        {
            PersonajesMapa.TryRemove(idPersonaje, out Tuple<IPersonaje, GameObject> tupla);
            CorutinasMovimientos.TryRemove(idPersonaje, out Coroutine movimiento);
            NodosPersonajes.TryRemove(idPersonaje, out Nodo nodo);
            if (movimiento != null) { StopCoroutine(movimiento); }
            if (tupla.Item2 != null) { Destroy(tupla.Item2); }
        }
    }

    public void MovimientoPersonaje(long idPersonaje, List<Nodo> nodos)
    {
        if (PersonajesMapa.ContainsKey(idPersonaje))
        {
            var personaje = PersonajesMapa[idPersonaje];

            if (CorutinasMovimientos.TryGetValue(idPersonaje, out Coroutine movimiento))
            {
                if (movimiento != null)
                {
                    StopCoroutine(movimiento);
                    CorutinasMovimientos[idPersonaje] = null;

                    if (NodosPersonajes[idPersonaje] != null)
                    {
                        if (Vector3.Distance(personaje.Item2.transform.position, new Vector3(NodosPersonajes[idPersonaje].X / 2f, 0f, NodosPersonajes[idPersonaje].Y / 2f)) <= 0.1f)
                        {
                            Vector2 posicionActual = new(personaje.Item1.EntidadCombate.X, personaje.Item1.EntidadCombate.Y);

                            personaje.Item1.EntidadCombate.X = (short)NodosPersonajes[idPersonaje].X;
                            personaje.Item1.EntidadCombate.Y = (short)NodosPersonajes[idPersonaje].Y;

                            personaje.Item2.GetComponent<GestorAnimaciones>().Play((byte)Tipos.Animaciones.MOVIMIENTO);

                            Vector3 eulerAngle = new(0f, ObtenerAnguloDeDireccion(ObtenerDireccion(posicionActual, new Vector2((short)NodosPersonajes[idPersonaje].X, (short)NodosPersonajes[idPersonaje].Y))), 0f);
                            Quaternion rotation = Quaternion.Euler(eulerAngle);

                            personaje.Item2.transform.rotation = rotation;
                        }
                    }
                }

                CorutinasMovimientos[idPersonaje] = StartCoroutine(MoverPersonaje(PersonajesMapa[idPersonaje], nodos));
            }
        }
    }

    private IEnumerator MoverPersonaje(Tuple<IPersonaje, GameObject> personaje, List<Nodo> nodos)
    {
        int index = 0;

        Vector3 posicionMarcada;
        Vector2 posicionActual = new(personaje.Item1.EntidadCombate.X, personaje.Item1.EntidadCombate.Y);

        while (index < nodos.Count)
        {
            personaje.Item2.GetComponent<GestorAnimaciones>().Play((byte)Tipos.Animaciones.MOVIMIENTO);

            posicionMarcada = new Vector3(nodos[index].X / 2f, 0f, nodos[index].Y / 2f); // A nivel físico.

            NodosPersonajes[personaje.Item1.IdPersonaje] = nodos[index];

            Vector3 eulerAngle = new(0f, ObtenerAnguloDeDireccion(ObtenerDireccion(posicionActual, new Vector2(posicionMarcada.x * 2, posicionMarcada.z * 2))), 0f);
            Quaternion rotation = Quaternion.Euler(eulerAngle);

            if (personaje.Item2 == null) { break; }

            personaje.Item2.transform.rotation = rotation;

            float step = personaje.Item1.EntidadCombate.Velocidad * Time.fixedDeltaTime;

            while (Vector3.Distance(personaje.Item2.transform.position, posicionMarcada) > 0.1f)
            {
                if (personaje.Item2 == null) { break; }
                personaje.Item2.transform.position = Vector3.MoveTowards(personaje.Item2.transform.position, posicionMarcada, step);
                yield return new WaitForFixedUpdate();
            }

            posicionActual.x = posicionMarcada.x * 2;
            posicionActual.y = posicionMarcada.z * 2;

            personaje.Item1.EntidadCombate.X = (short)posicionActual.x;
            personaje.Item1.EntidadCombate.Y = (short)posicionActual.y;

            index++;
        }

        if (personaje.Item2 != null)
        {

            if (nodos.Count > 0)
            {
                personaje.Item2.transform.position = new Vector3(nodos[index - 1].X / 2f, 0f, nodos[index - 1].Y / 2f);
            }

            personaje.Item2.GetComponent<GestorAnimaciones>().Play((byte)Tipos.Animaciones.IDLE);

            nodos.Clear();
            NodosPersonajes[personaje.Item1.IdPersonaje] = null;

        }
    }

    public int ObtenerDireccion(Vector2 origin, Vector2 destination)
    {
        Vector2 direction = new(destination.y - origin.y, destination.x - origin.x);
        int directionIndex = Mathf.RoundToInt(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg / 45f);
        if (directionIndex < 0)
        {
            directionIndex += 8;
        }
        return directionIndex + 1;
    }

    public float ObtenerAnguloDeDireccion(int direction)
    {
        return (direction - 1) * 45f;
    }

    public Vector2 ObtenerCoordenadas(Vector3 point)
    {
        if (point != null)
        {
            double x = System.Math.Round(point.x, 2);
            double z = System.Math.Round(point.z, 2);

            double mod_x = x % 0.5f;
            double mod_z = z % 0.5f;

            int final_x = (int)System.Math.Truncate(x / 0.5f);
            int final_z = (int)System.Math.Truncate(z / 0.5f);

            if (mod_x >= 0.25) { final_x++; }
            if (mod_z >= 0.25) { final_z++; }

            return new Vector2(final_x, final_z);
        }
        return new Vector2(-1, -1);
    }

    private void OnDestroy()
    {
        GestorDeEventos.EliminarPersonajeDeMapa -= EliminarPersonaje;
        GestorDeEventos.InstanciarNuevoPersonajeMapa -= AgregarPersonaje;
        GestorDeEventos.MovimientoPersonaje -= MovimientoPersonaje;
    }
}