using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float yOffset;
    private Vector3 offset;

	// Use this for initialization
	void Start () 
    {
        offset = new Vector3(0, yOffset, 0);
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
	}
}
