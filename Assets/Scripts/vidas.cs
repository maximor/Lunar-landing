using UnityEngine;
using System.Collections;

public class vidas : MonoBehaviour {
    private static int vida = 3;

    public static void setVidas(int v)
    {
        vida = v;
    }
    public static int getVidas()
    {
        return vida;
    }
}
