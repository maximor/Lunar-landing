using UnityEngine;
using System.Collections;

public class combustible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (gameObject.tag == "Gas")
        {
            Destroy(gameObject);
            colision.gameObject.SendMessageUpwards("darGas");
        }
    }
}
