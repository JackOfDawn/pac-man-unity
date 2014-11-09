using UnityEngine;
using System.Collections;

public abstract class BaseMovement : MonoBehaviour {

    public enum Direction
    {
        NORTH = 0,
        SOUTH,
        EAST,
        WEST,
        NONE
    }

    public Direction currentDirection = Direction.NORTH;
    protected Direction leftDirection;
    protected Direction rightDirection;
    protected Direction backDirection;
    protected Direction queuedDirection = Direction.NONE;

    protected bool canTurnLeft;
    protected bool canTurnRight;
    protected bool canMoveForward;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected void setDirections()
    {
        switch (currentDirection)
        {
            case Direction.NORTH:
                leftDirection = Direction.WEST;
                rightDirection = Direction.EAST;
                backDirection = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                leftDirection = Direction.EAST;
                rightDirection = Direction.WEST;
                backDirection = Direction.NORTH;
                break;
            case Direction.EAST:
                leftDirection = Direction.NORTH;
                rightDirection = Direction.SOUTH;
                backDirection = Direction.WEST;
                break;
            case Direction.WEST:
                leftDirection = Direction.SOUTH;
                rightDirection = Direction.NORTH;
                backDirection = Direction.EAST;
                break;
            default: //if currentDirection is none, then the player has stopped moving, no need to change anything;
                break;
        }
    }

    protected void checkBoundaries()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 left = transform.TransformDirection(Vector3.left);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 frontPos = transform.TransformDirection(Vector3.forward * transform.localScale.z * .7f) + transform.localPosition; //get the front middle of the cube
        Vector3 backPos = transform.TransformDirection(Vector3.back * transform.localScale.z * .9f) + transform.localPosition; // get the back middle
        int wallMask = 1 << LayerMask.NameToLayer("Wall");

        //check left boundaries
        bool frontLeftOpen = !Physics.Raycast(frontPos, left, 1.5f * transform.localScale.x, wallMask);
        bool midLeftOpen = !Physics.Raycast(transform.position, left, 1.5f * transform.localScale.x, wallMask);
        bool backLeftOpen = !Physics.Raycast(backPos, left, (1.5f * transform.localScale.x), wallMask);

        if (backLeftOpen && frontLeftOpen && midLeftOpen)
        {
            canTurnLeft = true;
        }
        else
        {
            canTurnLeft = false;
        }

        //check right boundaries
        bool frontRightOpen = !Physics.Raycast(frontPos, right, 1.5f * transform.localScale.x, wallMask);
        bool backRightOpen = !Physics.Raycast(backPos, right, 1.5f * transform.localScale.x, wallMask);
        bool midRightOpen = !Physics.Raycast(transform.position, right, 1.5f * transform.localScale.x, wallMask);

        if (frontRightOpen && backRightOpen && midRightOpen)
        {
            canTurnRight = true;
        }
        else
        {
            canTurnRight = false;
        }

        //check front boundaries
        if (!Physics.Raycast(transform.position, fwd, transform.localScale.x, wallMask))
        {
            canMoveForward = true;
        }
        else
        {
            canMoveForward = false;
        }

    }

    protected abstract void updateQueuedDirection();
    protected abstract void handleQueuedDirection();

}
