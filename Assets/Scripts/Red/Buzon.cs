using System;
using System.Collections.Generic;
using UnityEngine;

public class Buzon : MonoBehaviour
{
    public static Buzon instancia;

    private Queue<string> buzon;
    public event EventHandler NuevoMensaje;
    private static object locker;

    void Awake()
    {
        if(Buzon.instancia == null)
        {// No existía previamente ninguna instancia.
            Buzon.instancia = this;

            locker = new object();
            buzon = new Queue<string>();
        }
    }

    public void AgregarMensaje(string mensaje)
    {
        lock(locker)
        {
            buzon.Enqueue(mensaje);
        }

        OnNuevoMensaje();
    }

    protected virtual void OnNuevoMensaje()
    {
        NuevoMensaje?.Invoke(this, EventArgs.Empty);
    }


    public string CuantosHay()
    {
        lock(locker )
        {
            return buzon.Count.ToString();
        }
    }

    public bool HayMensajesEnCola()
    {
        lock(locker)
        {
            return buzon.Count > 0;
        }
    }

    public string ObtenerSiguienteMensaje()
    {
        lock(locker)
        {
            return buzon.Dequeue();
        }
    }
}