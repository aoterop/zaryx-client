using UnityEngine;

public class GestorApariencia : MonoBehaviour
{
    public GameObject[] cintos;
    public GameObject[] armaduras;
    public GameObject[] caras;
    public GameObject[] guantes;
    public GameObject[] peinados;
    public GameObject[] cascos;
    public GameObject[] mochilas;
    public GameObject[] armamentoIzq;
    public GameObject[] armamentoDcha;
    public GameObject[] botas;
    public GameObject[] hombreras;

    private byte ultimoCinto = 255;
    private byte ultimaArmadura = 255;
    private byte ultimaCara = 255;
    private byte ultimoGuante = 255;
    private byte ultimoPeinado = 255;
    private byte ultimoCasco = 255;
    private byte ultimaMochila = 255;
    private byte ultimoArmamentoIzq = 255;
    private byte ultimoArmamentoDcha = 255;
    private byte ultimaBota = 255;
    private byte ultimaHombrera = 255;

    private bool anotado = false;

    private void Anotaciones()
    {
        AnotarActivos(cintos, ref ultimoCinto);
        AnotarActivos(armaduras, ref ultimaArmadura);
        AnotarActivos(caras, ref ultimaCara);
        AnotarActivos(guantes, ref ultimoGuante);
        AnotarActivos(peinados, ref ultimoPeinado);
        AnotarActivos(cascos, ref ultimoCasco);
        AnotarActivos(mochilas, ref ultimaMochila);
        AnotarActivos(armamentoIzq, ref ultimoArmamentoIzq);
        AnotarActivos(armamentoDcha, ref ultimoArmamentoDcha);
        AnotarActivos(botas, ref ultimaBota);
        AnotarActivos(hombreras, ref ultimaHombrera);

        anotado = true;
    }

    private void AnotarActivos(GameObject[] componentes, ref byte ultimo)
    {
        byte pos = 0;
        foreach (GameObject go in componentes)
        {
            if (go.activeSelf)
            {
                ultimo = pos;
            }
            pos++;
        }
    }

    public void Equipar(Tipos.ComponentePersonaje tipo, string nombre)
    {
        if (!anotado)
        {
            Anotaciones();
        }
        switch (tipo)
        {
            case Tipos.ComponentePersonaje.CINTO: { EquiparComponente(cintos, ref ultimoCinto, nombre); } break;
            case Tipos.ComponentePersonaje.ARMADURA: { EquiparComponente(armaduras, ref ultimaArmadura, nombre); } break;
            case Tipos.ComponentePersonaje.CARA: { EquiparComponente(caras, ref ultimaCara, nombre); } break;
            case Tipos.ComponentePersonaje.GUANTE: { EquiparComponente(guantes, ref ultimoGuante, nombre); } break;
            case Tipos.ComponentePersonaje.PEINADO: { EquiparComponente(peinados, ref ultimoPeinado, nombre); } break;
            case Tipos.ComponentePersonaje.CASCO: { EquiparComponente(cascos, ref ultimoCasco, nombre); } break;
            case Tipos.ComponentePersonaje.MOCHILA: { EquiparComponente(mochilas, ref ultimaMochila, nombre); } break;
            case Tipos.ComponentePersonaje.ARMAMENTO_IZQ: { EquiparComponente(armamentoIzq, ref ultimoArmamentoIzq, nombre); } break;
            case Tipos.ComponentePersonaje.ARMAMENTO_DCHA: { EquiparComponente(armamentoDcha, ref ultimoArmamentoDcha, nombre); } break;
            case Tipos.ComponentePersonaje.BOTA: { EquiparComponente(botas, ref ultimaBota, nombre); } break;
            case Tipos.ComponentePersonaje.HOMBRERA: { EquiparComponente(hombreras, ref ultimaHombrera, nombre); } break;
        }
    }

    public void AplicarEstilos(byte aspectoFacial, byte peinado)
    {
        Equipar(Tipos.ComponentePersonaje.CARA, "Cara_" + aspectoFacial.ToString());
        Equipar(Tipos.ComponentePersonaje.PEINADO, "Pelo" + peinado.ToString());
    }

    private void TratarPelo(bool cortar)
    {// Refactorizar.
        if (ultimoPeinado != 255)
        {
            string nombrePeinado;

            if (cortar)
            {
                int posicion = peinados[ultimoPeinado].name.IndexOf('C');

                if (posicion >= 0)
                {// El pelo ya era corto.
                    nombrePeinado = peinados[ultimoPeinado].name;
                }
                else
                {
                    nombrePeinado = peinados[ultimoPeinado].name + "Corto";
                }
            }
            else
            {
                string peinadoCorto = peinados[ultimoPeinado].name;
                int posicion = peinados[ultimoPeinado].name.IndexOf('C');

                if (posicion >= 0)
                {
                    nombrePeinado = peinadoCorto[..posicion]; // = peinadoCorto.Substring(0, posicion);
                }
                else
                {// El pelo ya era largo.
                    nombrePeinado = peinados[ultimoPeinado].name;
                }
            }

            byte pos = 0;
            foreach (GameObject go in peinados)
            {
                if (go.name == nombrePeinado)
                {
                    peinados[ultimoPeinado].SetActive(false);
                    go.SetActive(true);
                    ultimoPeinado = pos;
                }
                pos++;
            }
        }
    }


    private void EquiparComponente(GameObject[] componentes, ref byte ultimoComponente, string nombre)
    {
        if (ultimoComponente != 255)
        {
            componentes[ultimoComponente].SetActive(false);
        }

        if (nombre != null || nombre != "")
        {
            byte pos = 0;

            foreach (GameObject go in componentes)
            {
                if (go.name == nombre)
                {
                    ultimoComponente = pos;
                    go.SetActive(true);
                }
                pos++;
            }
        }
    }

    public void TransformarGuerreroSM()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_G");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_G0");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_SM");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_G");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "G0");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "G0");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_SM");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "Hombrera_G0");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarGuerreroPrimera()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_G");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_G1");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_G1");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_G");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "G1");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_G1");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "Hombrera_G1");
        Equipar(Tipos.ComponentePersonaje.CASCO, "Casco_G1");

        TratarPelo(true);
    }

    public void TransformarGuerreroSegunda()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_G");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_G2");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_G2");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_G");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "G2");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "G2");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_G2");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "Hombrera_G2");
        Equipar(Tipos.ComponentePersonaje.CASCO, "Casco_G2");

        TratarPelo(true);
    }

    public void TransformarTiradorSM()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_T");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_T0");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_SM");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_T");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "T0");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "T");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_SM");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarTiradorPrimera()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_T");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_T1");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_T1");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_T");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "T1");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "T");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_T1");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarTiradorSegunda()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_T");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_T2");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_T2");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_T");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "T2");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "T");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_T2");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarMagoSM()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_M");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_M0");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_SM");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_M");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "M0");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_SM");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarMagoPrimera()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_M");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_M1");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_M1");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_M");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "M1");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_M1");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "");

        TratarPelo(false);
    }

    public void TransformarMagoSegunda()
    {
        Equipar(Tipos.ComponentePersonaje.CINTO, "Cinto_M");
        Equipar(Tipos.ComponentePersonaje.ARMADURA, "Armadura_M2");
        Equipar(Tipos.ComponentePersonaje.GUANTE, "Guantes_M2");
        Equipar(Tipos.ComponentePersonaje.MOCHILA, "Mochila_M");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_IZQ, "");
        Equipar(Tipos.ComponentePersonaje.ARMAMENTO_DCHA, "M2");
        Equipar(Tipos.ComponentePersonaje.BOTA, "Botas_M2");
        Equipar(Tipos.ComponentePersonaje.HOMBRERA, "");
        Equipar(Tipos.ComponentePersonaje.CASCO, "Casco_M2");

        TratarPelo(true);
    }
}