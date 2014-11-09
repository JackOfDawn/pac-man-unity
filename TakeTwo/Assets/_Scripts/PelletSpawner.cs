using UnityEngine;
using System.Collections;

public class PelletSpawner : MonoBehaviour {

    public Transform pellet;
    public float gridX = 0f;
    public float gridZ = 0f;
    public float spacing = 2f;
    Vector3 offset = new Vector3(0, .5f, 0);
	void Start () {
        for (int x = -7; x < gridX; x++)
        {
            for (int z = -7; z < gridZ; z++)
            {
                Vector3 spawnLoc = (new Vector3(x, 0, z) * spacing) + offset;
                Instantiate(pellet, spawnLoc, Quaternion.identity);
            }
        }
        
	}

}
