using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{

    public enum Direction
    {
        NORTH = 0,
        SOUTH,
        EAST,
        WEST,
        NONE
    }

    public float moveSpeed = 5;
    public Direction currentDirection = Direction.NORTH;
    private Direction queuedDirection = Direction.NONE;
    
    private Direction leftDirection;
    private Direction rightDirection;
    private Direction backDirection;

    private bool canTurnLeft;
    private bool canTurnRight;
    private bool canMoveForward;

	// Use this for initialization
	void Start () 
    {
        
        setDirections();
        canTurnLeft = false;
        canTurnRight = false;
        canMoveForward = false;
	}
	
	

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
           // this.currentDirection = Direction.NONE;
            //transform.Translate(Vector3.back * .2f);
        }
    }

    // Update is called once per frame
	void Update () 
    {
        checkBoundaries();
        if(!canMoveForward)
        {
            this.currentDirection = Direction.NONE;
        }
        updateQueuedDirection();
        handleQueuedDirection();
       
	   
        if(currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        
	}

    private void checkBoundaries()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 left = transform.TransformDirection(Vector3.left);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 frontPos = transform.TransformDirection(Vector3.forward * transform.localScale.z * .9f) + transform.localPosition; //get the front middle of the cube
        Vector3 backPos = transform.TransformDirection(Vector3.back * transform.localScale.z * .9f) + transform.localPosition; // get the back middle
        int wallMask = 1 << LayerMask.NameToLayer("Wall");
        int warpMask = 1 << LayerMask.NameToLayer("Warp");

        //check left boundaries
        bool frontLeftOpen = !Physics.Raycast(frontPos, left, 1.5f * transform.localScale.x, wallMask);
        bool midLeftOpen = !Physics.Raycast(transform.position, left, 1.5f * transform.localScale.x, wallMask);
        bool backLeftOpen = !Physics.Raycast(backPos, left, (1.5f * transform.localScale.x), wallMask);

        if(backLeftOpen && frontLeftOpen && midLeftOpen)
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

        if(frontRightOpen && backRightOpen && midRightOpen)
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

        if(Physics.Raycast(transform.position, fwd, transform.localScale.x, warpMask))
        {
            transform.position = new Vector3(-transform.position.x,transform.position.y, transform.position.z);
        }
        
    }

    private void setDirections()
    {
        switch(currentDirection)
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

    private void handleQueuedDirection()
    {
        bool turned = false;
        if(queuedDirection == Direction.NONE)
        {
            return; //no need to handle anything
        }
        
        if(queuedDirection == leftDirection && canTurnLeft)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            turned = true;
        }
        else if( queuedDirection == rightDirection && canTurnRight)
        {
            transform.Rotate(new Vector3(0, 90, 0));
            turned = true;
        }
        else if (queuedDirection == backDirection)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            turned = true;
        }

        if (turned)
        {
            currentDirection = queuedDirection;
            queuedDirection = Direction.NONE;
            setDirections();
        }
    }

    private void updateQueuedDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            queuedDirection = Direction.WEST; 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            queuedDirection = Direction.EAST;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            queuedDirection = Direction.NORTH;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            queuedDirection = Direction.SOUTH;
        }
        
    }

}
