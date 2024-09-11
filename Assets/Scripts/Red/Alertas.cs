using System.Collections.Generic;

public static class Alertas
{
    public static readonly Dictionary<string, string> Mensajes = new()
    {
        { "ConexionPerdida", "Fallo al conectar con el servidor" },
        { "CuentaEnUso", "Esta cuenta ya est� en uso" },
        { "CredencialesIncorrectas", "Las credenciales son incorrectas"},
        { "CuentaInexistente", "La cuenta no existe" },
        { "NombreInvalido", "El nombre es inv�lido" },
        { "NombreEnUso", "Este nombre ya est� en uso" },
        { "CuentaInactiva", "La cuenta no est� activada" },
        { "CuentaBaneada", "La cuenta est� baneada" },
        { "BorrarPersonaje", "�Desea borrar este personaje?" }
    };
}