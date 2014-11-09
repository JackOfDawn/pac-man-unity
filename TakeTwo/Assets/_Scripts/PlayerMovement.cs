using UnityEngine;
using System.Collections;

public class PlayerMovement : BaseMovement 
{

    public float moveSpeed = 5;
    public Vector3 startPos;

	void Start () 
    {
        transform.position = startPos;
        setDirections();
        canTurnLeft = false;
        canTurnRight = false;
        canMoveForward = false;
	}
	
    void OnCollisionEnter(Collision col)
    {
        //use this for the ghosts
    }

    // Update is called once per frame
	void Update () 
    {
        checkBoundaries();
        if(!canMoveForward)
        {
            currentDirection = Direction.NONE;
        }
        updateQueuedDirection();
        handleQueuedDirection();
       
	   
        if(currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        
	}

  
    protected override void handleQueuedDirection()
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

    protected override void updateQueuedDirection()
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
