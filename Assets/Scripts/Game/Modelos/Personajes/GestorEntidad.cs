using UnityEngine;

public class GestorEntidad : MonoBehaviour
{
    public void Posicionar(float logico_x, float logico_y)
    {
        Vector2 puntoFinal = PuntoLogicoAFisico(new Vector2(logico_x, logico_y));
        transform.position = new Vector3(puntoFinal.x, 0, puntoFinal.y);
    }

    public Vector2 PuntoFisicoALogico(Vector2 fisico)
    {
        return fisico * 2;
    }

    public Vector2 PuntoLogicoAFisico(Vector2 logico)
    {
        return logico / 2;
    }

    private Vector2 ObtenerPuntoLogico(Vector3 point)
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
}