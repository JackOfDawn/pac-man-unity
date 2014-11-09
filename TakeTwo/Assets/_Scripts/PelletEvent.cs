using UnityEngine;
using System.Collections;

public class PelletEvent : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<GeneratePitchSound>().handleSound();
            Destroy(gameObject);
        }
    }
}
