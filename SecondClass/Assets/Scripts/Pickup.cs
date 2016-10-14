using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public GameObject particleSystem;
    public Collider ourCollider;
    public Renderer ourRenderer;
	// Use this for initialization
	void Start () {
       
	}
    public void Death()
    {
        GameObject particles = GameObject.Instantiate(particleSystem, transform.position, Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
        ourCollider.enabled = false;
        ourRenderer.enabled = false;
        GameObject.Destroy(particles, 4.5f);
        GameObject.Destroy(this.gameObject, 5);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
