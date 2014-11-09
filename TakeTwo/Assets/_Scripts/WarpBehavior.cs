using UnityEngine;
using System.Collections;

public class WarpBehavior : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 pos = other.transform.position;
            pos.x = -pos.x;
            other.transform.position = pos;
        }
    }
}
