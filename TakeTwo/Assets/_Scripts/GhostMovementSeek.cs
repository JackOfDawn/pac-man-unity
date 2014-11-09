using UnityEngine;
using System.Collections;

public class GhostMovementSeek : BaseMovement {



    public float moveSpeed = 5;
    public Vector3 startPos;
    public GameObject Player;

    private Vector3 target;

    private bool isSeeking;

	// Use this for initialization
	void Start () 
    {
        target = Player.transform.position;
        transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () 
    {

        Vector3 newTarget = (target - transform.position);
        newTarget.Normalize();
        transform.Translate(newTarget * Time.deltaTime * moveSpeed);

        target = Player.transform.position;
	}

    protected override void updateQueuedDirection()
    {

    }

    protected override void handleQueuedDirection()
    {

    }
}
