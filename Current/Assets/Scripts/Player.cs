using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    public Rigidbody ourRb;
    public float moveSpeed;
    public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveH, 0, moveV);
        ourRb.AddForce(moveDirection * moveSpeed);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Pickup")
        {
            Destroy(collision.gameObject);
            this.score++;
        }
    }
}
