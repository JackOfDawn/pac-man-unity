using UnityEngine;
using System.Collections;

public class GhostMovementWander : BaseMovement {

    public float moveSpeed = 5;
    public Vector3 startPos;

    private const float TIME_FOR_NEW_DIRECTION = 1f;
    private float cooldown;

    private const int FORWARD = 0;
    private const int LEFT = -1;
    private const int RIGHT = 1;
    private int desiredDirection;

	// Use this for initialization
	void Start () 
    {
        transform.position = startPos;
        setDirections();
        canTurnLeft = false;
        canTurnRight = false;
        canMoveForward = false;
        cooldown = TIME_FOR_NEW_DIRECTION;
	}
	
	// Update is called once per frame
	void Update () 
    {
        checkBoundaries();
        if (!canMoveForward)
        {
            currentDirection = Direction.NONE;
        }

        cooldown -= Time.deltaTime;
        if(cooldown <= 0) //pick a new desiredDirection;
        {
            cooldown = TIME_FOR_NEW_DIRECTION;
            updateQueuedDirection();
        }

        handleQueuedDirection();

        if (currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }

    protected override void updateQueuedDirection()
    {
        int newDirection = Random.Range(-1, 1);

        switch (newDirection)
        {
            case LEFT:
                queuedDirection = leftDirection;
                break;
            case RIGHT:
                queuedDirection = rightDirection;
                break;
            default:
                queuedDirection = Direction.NONE;
                break;
        }
    }
    protected override void handleQueuedDirection()
    {
        bool turned = false;
        if (currentDirection != Direction.NONE)
        {
            if (queuedDirection == Direction.NONE)
            {
                return; //no need to handle anything
            }

            if (queuedDirection == leftDirection && canTurnLeft)
            {
                transform.Rotate(new Vector3(0, -90, 0));
                turned = true;
            }
            else if (queuedDirection == rightDirection && canTurnRight)
            {
                transform.Rotate(new Vector3(0, 90, 0));
                turned = true;
            }
        }
        else
        {
            if (canTurnLeft)
            {
                transform.Rotate(new Vector3(0, -90, 0));
                queuedDirection = leftDirection;
                turned = true;
            }
            else if (canTurnRight)
            {
                transform.Rotate(new Vector3(0, 90, 0));
                queuedDirection = rightDirection;
                turned = true;
            }
            else //if ghost can't turn right and can't turn left or move forward
            {
                transform.Rotate(new Vector3(0, 180, 0));
                queuedDirection = backDirection;
                turned = true;
            }
        }


        if (turned)
        {
            currentDirection = queuedDirection;
            queuedDirection = Direction.NONE;
            setDirections();
        }
    }
}
