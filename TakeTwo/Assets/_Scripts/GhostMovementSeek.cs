using UnityEngine;
using System.Collections;

public class GhostMovementSeek : BaseMovement {



    public float moveSpeed = 5;
    public Vector3 startPos;
    public GameObject Player;
    private Vector3 target;



	// Use this for initialization
	void Start () 
    {
        target = Player.transform.position;
        transform.position = startPos;
        setDirections();
        canTurnLeft = false;
        canTurnRight = false;
        canMoveForward = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        target = Player.transform.position;
        checkBoundaries();
        if (!canMoveForward)
        {
            currentDirection = Direction.NONE;
        }
        updateQueuedDirection();
        handleQueuedDirection();
        //Vector3 newTarget = (target - transform.position);
        //newTarget.Normalize();
        //transform.Translate(newTarget * Time.deltaTime * moveSpeed);

        

        if(currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
	}

    protected override void updateQueuedDirection()
    {
        float[] distance = {-1,-1,-1}; //forward, left right
        
        //check forward
        if(canMoveForward)
        {
            Vector3 forwardVector = transform.TransformDirection(Vector3.forward * transform.localScale.x) + transform.position;
            distance[0] = Vector3.Distance(forwardVector, target);
        }
        if(canTurnLeft)
        {
            Vector3 leftVector = transform.TransformDirection(Vector3.left * transform.localScale.x) + transform.position;
            distance[1] = Vector3.Distance(leftVector, target);
        }
        if(canTurnRight)
        {
            Vector3 rightVector = transform.TransformDirection(Vector3.right * transform.localScale.x) + transform.position;
            distance[2] = Vector3.Distance(rightVector, target);
        }

        int index = 0;
        float smallestDistance = int.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            if(distance[i] != -1 && distance[i] < smallestDistance)
            {
                index = i;
                smallestDistance = distance[i];
            }
        }

        switch(index)
        {
            case 1: //left
                queuedDirection = leftDirection;
                break;
            case 2: //right
                queuedDirection = rightDirection;
                break;
            default:
                queuedDirection = currentDirection;
                break;
        }


    }

    protected override void handleQueuedDirection()
    {
        bool turned = false;
        if(currentDirection!= Direction.NONE)
        {
            if (queuedDirection == Direction.NONE)
            {
                return;
            }
            else if (queuedDirection == leftDirection)
            {
                transform.Rotate(new Vector3(0, -90, 0));
                turned = true;
            }
            else if (queuedDirection == rightDirection)
            {
                transform.Rotate(new Vector3(0, 90, 0));
                turned = true;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, 180, 0));
            queuedDirection = backDirection;
            turned = true;
        }

        if (turned)
        {
            currentDirection = queuedDirection;
            queuedDirection = Direction.NONE;
            setDirections();
        }
    }
}
