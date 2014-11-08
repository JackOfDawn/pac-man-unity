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
            this.currentDirection = Direction.NONE;
            transform.Translate(Vector3.back * .2f);
        }
    }

    // Update is called once per frame
	void Update () 
    {
        checkBoundaries();
        updateQueuedDirection();
        handleQueuedDirection();
       
	   
        if(currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        
	}

    private void checkBoundaries()
    {
        Vector3 left = transform.TransformDirection(Vector3.left);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 frontPos = transform.TransformDirection(Vector3.forward * transform.localScale.z/2) + transform.localPosition; //get the front middle of the cube
        Vector3 backPos = transform.TransformDirection(Vector3.back * transform.localScale.z / 2) + transform.localPosition; // get the back middle
        //check left boundaries
        bool frontLeftOpen = !Physics.Raycast(frontPos, left, transform.localScale.x);
        bool backLeftOpen = !Physics.Raycast(backPos, left, transform.localScale.x);

        if(backLeftOpen && frontLeftOpen)
        {
            canTurnLeft = true;
        }
        else
        {
            canTurnLeft = false;
        }
        /*
        if(frontLeftOpen)
            Debug.DrawRay(frontPos, left);
        else
            Debug.DrawRay(frontPos, left, Color.red);

        if(backLeftOpen)
            Debug.DrawRay(backPos,left);
        else
            Debug.DrawRay(backPos, left, Color.red);
        */
        
        //check right boundaries
        bool frontRightOpen = !Physics.Raycast(frontPos, right, transform.localScale.x);
        bool backRightOpen = !Physics.Raycast(backPos, right, transform.localScale.x);

        if(frontRightOpen && backRightOpen)
        {
            canTurnRight = true;
            print("can turn right");
        }
        else
        {
            canTurnRight = false;
            print("can't turn right");
        }

        //check front boundaries
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
        
        
        //throw new System.NotImplementedException();
    }

    private void updateQueuedDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            queuedDirection = Direction.WEST;
         
            //transform.rotation = Quaternion.Euler(new Vector3(0,-90,0)); //go left
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            queuedDirection = Direction.EAST;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); //go right
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            queuedDirection = Direction.NORTH;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); //north
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            queuedDirection = Direction.SOUTH;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); //go south
        }
        
    }

}
