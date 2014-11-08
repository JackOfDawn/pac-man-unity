using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float yOffset;
    private Vector3 offset;

	void Start () 
    {
        offset = new Vector3(0, yOffset, 0);
	}
	
	void LateUpdate () 
    {
        transform.position = new Vector3(0, player.transform.position.y + yOffset, player.transform.position.z);
	}
}
