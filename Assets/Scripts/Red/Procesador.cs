using System;
using UnityEngine;

public class Procesador : MonoBehaviour
{
    public static Procesador Instancia { get; private set; }
    private bool procesando;
    private ManejadorDeMensajes manejador;

    private void Start()
    {
        if(Instancia == null)
        {
            Instancia = this;
            Buzon.instancia.NuevoMensaje += ProcesarSiguienteMensaje!;
            procesando = false;
            manejador = ManejadorDeMensajes.Instancia;
            manejador.RegistrarGestores();
        }
    }

    private void ProcesarSiguienteMensaje(object sender, EventArgs e)
    {
        lock(this)
        {
            if (!procesando) 
            {
                procesando = true;
                ProcesarMensajesEnCola();
            }
        }
    }

    private void ProcesarMensajesEnCola()
    {
        while(Buzon.instancia.HayMensajesEnCola())
        {
            string mensaje = Buzon.instancia.ObtenerSiguienteMensaje();

            string[] partesMensaje = mensaje.Split('§');
            manejador.ManejarMensaje(byte.Parse(partesMensaje[0]), partesMensaje[1]); // Tipo y mensaje.
        }

        procesando = false;
    }
}