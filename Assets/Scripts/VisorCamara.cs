using UnityEngine;
using System.Collections;

public class VisorCamara : MonoBehaviour {

	public Transform camaraFijaObjetivo;

	public float velocidadSeguimiento;
	public float profundidadCamaraZ = -10f;
    public GameObject vidaNave;
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
    bool estaCreado;
    int j;
    void Awake()
    {
        for (int i = 0; i < vidas.getVidas(); i++)
           Instantiate(vidaNave, new Vector3(transform.position.x + i, transform.position.y + 3.5f, 0), Quaternion.identity);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 campos = transform.position;

        foreach(GameObject vida in GameObject.FindGameObjectsWithTag("vidas"))
        {
            vida.transform.position = new Vector3(campos.x + j--, campos.y + 3.5f, 0);
        }
        j = vidas.getVidas();

		if (camaraFijaObjetivo != null) {
			var nuevaPosicion = Vector2.Lerp(transform.position, camaraFijaObjetivo.transform.position, Time.deltaTime * velocidadSeguimiento);
			var posicionCamara = new Vector3(nuevaPosicion.x,nuevaPosicion.y, profundidadCamaraZ);

				var var3 = posicionCamara;
				var nuevaX = Mathf.Clamp(var3.x,minX,maxX);
				var nuevaY = Mathf.Clamp(var3.y,minY,maxY);
				transform.position = new Vector3(nuevaX,nuevaY,profundidadCamaraZ);
		}
	}

    public void reducirVidas()
    {
        
        GameObject[] objVidas = GameObject.FindGameObjectsWithTag("vidas");
        Destroy(objVidas[vidas.getVidas() - 1]);
        vidas.setVidas(vidas.getVidas() - 1);
    }
}
