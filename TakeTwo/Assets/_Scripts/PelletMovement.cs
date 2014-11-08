using UnityEngine;
using System.Collections;

public class PelletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(45, 0, -45) * Time.deltaTime);
	}
}
