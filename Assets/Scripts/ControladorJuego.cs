using UnityEngine;
using System.Collections;

public class ControladorJuego : MonoBehaviour {
	public void ReiniciarNivel()
	{
        if (Application.loadedLevelName == "LunarLanding" || vidas.getVidas() == 0)
        {
            if(vidas.getVidas() == 0)
                vidas.setVidas(3);
            Application.LoadLevel(0);
        }

        //else if (Application.loadedLevelName == "LunarLanding")
        //    Application.LoadLevel(0);

        else if (Application.loadedLevelName == "nivel2")
            Application.LoadLevel(1);

        else if (Application.loadedLevelName == "nivel3")
            Application.LoadLevel(2);

        else if (Application.loadedLevelName == "nivel4")
            Application.LoadLevel(3);


    }

}
