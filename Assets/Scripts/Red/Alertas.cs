using System.Collections.Generic;

public static class Alertas
{
    public static readonly Dictionary<string, string> Mensajes = new()
    {
        { "ConexionPerdida", "Fallo al conectar con el servidor" },
        { "CuentaEnUso", "Esta cuenta ya está en uso" },
        { "CredencialesIncorrectas", "Las credenciales son incorrectas"},
        { "CuentaInexistente", "La cuenta no existe" },
        { "NombreInvalido", "El nombre es inválido" },
        { "NombreEnUso", "Este nombre ya está en uso" },
        { "CuentaInactiva", "La cuenta no está activada" },
        { "CuentaBaneada", "La cuenta está baneada" },
        { "BorrarPersonaje", "¿Desea borrar este personaje?" }
    };
}