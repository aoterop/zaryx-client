using UnityEngine;
using System.Net.Sockets; // Comunicación por red.
using System;
using System.Text;
using System.Collections;

public class GestorDeRed : MonoBehaviour
{
    public static GestorDeRed Red;
    private TcpClient Cliente { get; set; }
    private NetworkStream Stream { get; set; }
    private Int32 PuertoConexion { get; set; }
    private string IpServidor { get; set; }

    private bool estaConectado;
    private bool conectando;

    private void Awake()
    {// Aplicación del patrón Singleton en el Gestor de Red.

        if (GestorDeRed.Red == null)
        {// No existía previamente ninguna instancia.
            GestorDeRed.Red = this;
            GestorDeEventos.ConectarConServidor += Conectar;
            DontDestroyOnLoad(gameObject);
            estaConectado = false;
            conectando = false;
            Conectar();
        }
        else
        {// Ya existía una instancia previa.
            Destroy(gameObject);
        }
    }

    public bool ObtenerEstadoConexion() { return estaConectado; }

    public void CambiarEstadoConexion(bool nuevoEstado)
    {
        estaConectado = nuevoEstado;
        if(!estaConectado) { conectando = false; }
        Supervisor.Instancia.Supervisar(estaConectado);
    }

    public void Conectar()
    {
        if (!estaConectado && !conectando)
        {
            conectando = true;
            StartCoroutine(Inicializar());
        }
    }

    private IEnumerator Inicializar()
    {
        bool conexionExitosa = false;

        PuertoConexion = 17040;
        IpServidor = "127.0.0.1";

        Cliente = new TcpClient();

        var conexionTask = Cliente.ConnectAsync(IpServidor, PuertoConexion);

        // Esperar un máximo de 4 segundos para establecer la conexión.
        float tiempoEspera = 4f;
        float tiempoTranscurrido = 0f;
        while (!conexionTask.IsCompleted && tiempoTranscurrido < tiempoEspera)
        {
            yield return null;
            tiempoTranscurrido += Time.deltaTime;
        }

        if (conexionTask.IsCompleted)
        {
            if (Cliente.Connected)
            {
                Stream = Cliente.GetStream();

                CambiarEstadoConexion(true);
                conexionExitosa = true;
            }
            else
            {
                CambiarEstadoConexion(false);
            }
        }

        if (conexionExitosa)
        {
            yield return StartCoroutine(EscucharMensajes());
        }
    }

    public void EnviarMensaje(string mensaje)
    {
        byte[] bytesMensaje = Encoding.UTF8.GetBytes(mensaje);
        int longitudMensaje = bytesMensaje.Length;
        byte[] buffer = new byte[2 + longitudMensaje];
        byte[] cabecera = BitConverter.GetBytes(longitudMensaje);
        Array.Copy(cabecera, buffer, 2);
        Array.Copy(bytesMensaje, 0, buffer, 2, longitudMensaje);

        try
        {
            if (Stream.CanWrite && Cliente.Connected)
            {
                Stream.Write(buffer, 0, buffer.Length);
            }
        }
        catch (SocketException socketException)
        {
            if (socketException.ErrorCode == 10054)
            {// Código de error WSAECONNRESET para la interrupción forzada por el host remoto.
                Debug.Log("Error de conexión forzada por el host remoto.");
            }
            else
            {
                Debug.Log("Socket exception: " + socketException);
            }
            CambiarEstadoConexion(false);
        }
        catch (Exception e)
        {
            CambiarEstadoConexion(false);
            Debug.Log("Unexpected exception: " + e);
        }
    }

    private IEnumerator EscucharMensajes()
    {
        NetworkStream networkStream = Cliente.GetStream();
        ushort longitudMensaje;
        byte[] headerBuffer = new byte[2];
        byte[] buffer;
        string mensaje;

        while(estaConectado)
        {
            if (networkStream.DataAvailable)
            {  
                // Lee la longitud del mensaje:
                networkStream.Read(headerBuffer, 0, 2);
                longitudMensaje = BitConverter.ToUInt16(headerBuffer, 0);

                // Lee el contenido del mensaje a partir de su longitud:
                buffer = new byte[longitudMensaje];
                int bytesLeidos = networkStream.Read(buffer, 0, longitudMensaje);

                if (bytesLeidos > 0)
                {
                    mensaje = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                    if(mensaje == "OFF")
                    {
                        break;
                    }
                    else
                    {
                        Buzon.instancia.AgregarMensaje(Encoding.UTF8.GetString(buffer, 0, bytesLeidos));
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {// No hay datos disponibles para leer, rendimos la ejecución de vuelta a Unity.
                yield return null;
            }
        }
        CerrarConexion();
    }

    public void CerrarConexion()
    {
        CambiarEstadoConexion(false);

        Stream?.Close();
        Cliente?.Close();
    }

    private void OnDestroy()
    {
        GestorDeEventos.ConectarConServidor -= Conectar;
        CerrarConexion();
    }
}