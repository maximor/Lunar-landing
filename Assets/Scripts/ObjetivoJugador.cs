using UnityEngine;
using System.Collections;

public class ObjetivoJugador : MonoBehaviour {

	private bool contraerPistaAterrizaje;
	private bool contactoHecho;
	private Jugador naveJugador;

	// Use this for initialization
	void Start () {
		naveJugador = GameObject.Find ("Lander").GetComponent<Jugador> ();
		if (naveJugador == null) {
			Debug.LogError ("No se encuentra la nave del jugador. Asegurate de que el jugador sea llamado Jugador");
		}
	}

	public void ActivarAterrizaje()
	{
		Debug.Log ("Pista de aterrizaje Activada");
		if (contraerPistaAterrizaje == false && contactoHecho == false) {
			StartCoroutine (EsconderPies());
            if (Application.loadedLevelName == "LunarLanding")
                Application.LoadLevel(1);

            else if (Application.loadedLevelName == "nivel2")
                Application.LoadLevel(2);

            else if (Application.loadedLevelName == "nivel3")
                Application.LoadLevel(3);
        }
        
        
	}

	private IEnumerator EsconderPies()
	{
		contraerPistaAterrizaje = true;
		contactoHecho = true;
		yield return new WaitForSeconds (0.5f); //yield indica que lo que recibe el metodo como accesor u operador es un iterador.
		contraerPistaAterrizaje = false;
		naveJugador.propulsorActivado = false;
		GameObject.Find ("Lander").GetComponent<Jugador>().ActivarBotonReinicio ();
	}
			
	// Update is called once per frame
	void Update () {
		if (contraerPistaAterrizaje == true) {
			var PadAterrizajePosY = transform.position.y - Time.deltaTime / 3;
			transform.position = new Vector2 (transform.position.x, PadAterrizajePosY);
		}
	}
}
