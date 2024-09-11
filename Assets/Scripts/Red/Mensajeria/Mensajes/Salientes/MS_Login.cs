public class MS_Login : IMensajeSaliente
{
    public string Usuario { get; set; }
    public string HashContra { get; set; }

    public MS_Login(string usuario, string hashContra)
    {
        Usuario = usuario;
        HashContra = hashContra;
    }

    public byte Tipo() { return (byte)Tipos.MensajeSaliente.MS_LOGIN; } // Login : código 0.
}