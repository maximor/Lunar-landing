using UnityEngine;
using System.Collections;

public class PiesNave : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D colision)
	{
		if (colision.gameObject.tag == "LanderObjective")
        {
			var objetivoJugador = colision.gameObject.GetComponent<ObjetivoJugador> ();
			if (objetivoJugador != null)
            { 
				objetivoJugador.ActivarAterrizaje ();
			}
		}
	}

}
