using TMPro;
using UnityEngine;

public class Mensaje : MonoBehaviour
{
    private MensajeChat MensajeChat { get; set; }


    public void Inicializar(MensajeChat mensaje, bool global)
    {
        MensajeChat = mensaje;

        if(global && mensaje.Mapa != "")
        {
            GetComponent<TextMeshProUGUI>().text = "[" + mensaje.Mapa + "] " + mensaje.NombreEmisor + " : " + mensaje.Texto;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = mensaje.NombreEmisor + ": " + mensaje.Texto;
        }
    }

    public void Susurrar()
    {
        GestorIU.Instancia.Susurrar(MensajeChat);
    }
}

public class MensajeChat
{
    public string Texto { get; set; }
    public long PersonajeEmisor { get; set; }
    public string NombreEmisor { get; set; }
    public byte Tipo { get; set; }
    public string Mapa { get; set; }

    public MensajeChat(string texto, long personajeEmisor, string nombreEmisor, byte tipo, string mapa)
    {
        Texto = texto;
        PersonajeEmisor = personajeEmisor;
        NombreEmisor = nombreEmisor;
        Tipo = tipo;
        Mapa = mapa;
    }


    public MensajeChat Clone()
    {
        return new MensajeChat
        {
            Texto = Texto,
            NombreEmisor = NombreEmisor,
            PersonajeEmisor = PersonajeEmisor,
            Tipo = Tipo
        };
    }

    public MensajeChat() { }
}