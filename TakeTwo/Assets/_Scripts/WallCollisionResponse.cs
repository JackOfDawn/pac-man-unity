using UnityEngine;
using System.Collections;

public class WallCollisionResponse : MonoBehaviour 
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerMovement tmpMovement = col.gameObject.GetComponent<PlayerMovement>();
            tmpMovement.currentDirection = PlayerMovement.Direction.NONE;

            col.gameObject.transform.Translate(Vector3.back * .2f);
        }
    }

}
