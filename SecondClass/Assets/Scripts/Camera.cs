using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
    public Transform player;
    public Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.position + offset;
	}
}
