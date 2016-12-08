using UnityEngine;
using System.Collections;

public class Rotacion : MonoBehaviour {

	public bool habilitarRotacion = false;
	public float velocidadRotacion = 0f;
	private Transform cacheRotacion;

	// Use this for initialization
	void Start () {
		cacheRotacion = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (habilitarRotacion) 
		{
			if (cacheRotacion.GetComponent<Renderer> () != null) 
			{
				cacheRotacion.Rotate (0, 0, velocidadRotacion * Time.deltaTime, Space.Self); 
			}
		}
	}
}
