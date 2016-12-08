using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Jugador : MonoBehaviour {

	public Transform propulsorCentral;
	public Transform propulsorIzq;
	public Transform propulsorDer;
  
	public float poderPropulsorCentral;
	public float poderPropulsorLateral;
	public float gas;
    public Camera camara;
	public GameObject exploxionPrefab;
	public GameObject piesNave;
  
	public bool propulsorActivado; 
	private bool piesDesplegados; 
	private bool sonidoPropulsorActivado; 

	public Text textoGas;

	public Animator animacionPropulsorCentral;
	public Animator animacionPropulsorIzq;
	public Animator animacionPropulsorDer;
	public AudioSource audioPropulsor; 

	private GameObject objetivoNave; 

	private Rigidbody2D jugadorRB; 

	private HingeJoint2D unionPies; // permite que un objeto con rigidbody rote respecto a un punto.

	private Button botonReinicio;


	// Use this for initialization
	void Start () {
       

		jugadorRB = GetComponent<Rigidbody2D> ();
        gas = 15;
		objetivoNave = GameObject.Find ("LanderObjective");
		unionPies = transform.FindChild ("LanderFeet").GetComponent<HingeJoint2D>();
		botonReinicio = GameObject.Find ("RestartButton").GetComponent<Button> ();
		botonReinicio.onClick.AddListener (Reinicio);
	}

	void Reinicio()
	{
		botonReinicio.gameObject.GetComponent<Image> ().enabled = false;
		botonReinicio.gameObject.transform.FindChild ("Text").GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        
        //Despliegue de los pies de la nave
        var distanciaObjetivo = Vector2.Distance (transform.position, objetivoNave.transform.position);
		if (distanciaObjetivo <= 1f && piesDesplegados == false) 
		{
			piesDesplegados = true;
		}

		if (piesDesplegados == true) 
		{
			var piesNavePosY = unionPies.anchor.y + Time.deltaTime / 3;

			if (piesNavePosY < 0.38f) {
				unionPies.anchor = new Vector2 (0f, piesNavePosY);
			} else {
				piesDesplegados = false;
			}
		}

		//Activar sonidos
		if (sonidoPropulsorActivado == true) {
			TocarSonidoPropulsor ();
		} else {
			audioPropulsor.Pause ();
		}
	}

	void FixedUpdate()
	{
		sonidoPropulsorActivado = false;

		//Movimiento de la nave
		if (Input.GetAxis ("Vertical") > 0) {
			sonidoPropulsorActivado = true;
			AplicandoFuerza (propulsorCentral, poderPropulsorCentral);
			if (animacionPropulsorCentral != null && animacionPropulsorCentral.runtimeAnimatorController != null) {
				animacionPropulsorCentral.SetBool ("ActivandoPropulsor", true);
			}
		} else {
			if (animacionPropulsorCentral != null && animacionPropulsorCentral.runtimeAnimatorController != null) {
				animacionPropulsorCentral.SetBool ("ActivandoPropulsor", false);
			}
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			sonidoPropulsorActivado = true;
			AplicandoFuerza (propulsorIzq, poderPropulsorLateral);
			if (animacionPropulsorIzq != null && animacionPropulsorIzq.runtimeAnimatorController != null) {
				animacionPropulsorIzq.SetBool ("ActivandoPropulsor", true);
			}
		} else {
			if (animacionPropulsorIzq != null && animacionPropulsorIzq.runtimeAnimatorController != null) {
				animacionPropulsorIzq.SetBool ("ActivandoPropulsor", false);
			}
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			sonidoPropulsorActivado = true;
			AplicandoFuerza (propulsorDer, poderPropulsorLateral);
			if (animacionPropulsorDer != null && animacionPropulsorDer.runtimeAnimatorController != null) {
				animacionPropulsorDer.SetBool ("ActivandoPropulsor", true);
			}
		} else {
			if (animacionPropulsorDer != null && animacionPropulsorDer.runtimeAnimatorController != null) {
				animacionPropulsorDer.SetBool ("ActivandoPropulsor", false);
			}
		}

	}


	private void TocarSonidoPropulsor()
	{
		if (audioPropulsor.isPlaying) {
			return;
		}

		audioPropulsor.Play ();
	}

	void AplicandoFuerza(Transform propulsorTransform, float poderPropulsor)
	{
		if (propulsorActivado && gas > 0f) {
			//Aplicando la fuerza a la nave para simular el movimiento
			Vector3 direccion = transform.position - propulsorTransform.position;
			jugadorRB.AddForceAtPosition (direccion.normalized * poderPropulsor, propulsorTransform.position);

            //El movimiento disminulle la gasolina
           
            
			gas -= 0.01f;
			textoGas.text = "Gas " + Mathf.Round (gas);
		}
	}

	private void OnCollisionEnter2D(Collision2D colision)
	{

		if (colision.relativeVelocity.magnitude > 3) {
            camara.SendMessage("reducirVidas");
            DestructorNave ();
		} else if (colision.relativeVelocity.magnitude > 1) {
            camara.SendMessage("reducirVidas");
            DestructorNave ();
		}
       
    }

    //private void OnTriggerEnter2D(Collider2D colision)
    //{

    //    if (colision.gameObject.tag == "Gas")
    //    {
    //        Debug.Log("Entro");
    //        gas += 10f;
    //        Destroy(colision.gameObject);
    //    }
    //}

    private void DestructorNave()
	{
		if (exploxionPrefab != null) {
			var explocion = Instantiate (exploxionPrefab, transform.position, Quaternion.identity) as GameObject;
			Destroy (explocion, 1f);
		}

		Destroy (gameObject);
		ActivarBotonReinicio ();
	}

	public void ActivarBotonReinicio()
	{
		botonReinicio.gameObject.GetComponent<Image> ().enabled = true;
		botonReinicio.gameObject.transform.FindChild ("Text").GetComponent<Text> ().enabled = true;
        if(vidas.getVidas() == 0)
            botonReinicio.gameObject.transform.FindChild("Text").GetComponent<Text>().text = "Fin del juego";
        else
        {
            botonReinicio.gameObject.transform.FindChild("Text").GetComponent<Text>().text = "Reiniciar";
        }
    }

    public void darGas()
    {
        gas += 10;
    }
}
