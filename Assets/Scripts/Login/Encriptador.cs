using System.Security.Cryptography;
using System.Text;

public static class Encriptador
{
    public static string ObtenerSHA512(string texto)
    {
        using SHA512 sha512 = SHA512.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(texto);
        byte[] hash = sha512.ComputeHash(bytes);
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }
}