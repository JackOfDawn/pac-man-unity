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

	// Use this for initialization
	void Start () 
    {
        queuedDirection = currentDirection;
	}
	
	

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
            //this.currentDirection = Direction.NONE;
        }
    }

    // Update is called once per frame
	void Update () 
    {
        updateQueuedDirection();
        handleQueuedDirection();
       
	   
        if(currentDirection != Direction.NONE)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        
	}

    private void handleQueuedDirection()
    {
        if(queuedDirection == Direction.NONE)
        {
            return; //no need to handle anything
        }
        currentDirection = queuedDirection;
        queuedDirection = Direction.NONE;
        
        
        //throw new System.NotImplementedException();
    }

    private void updateQueuedDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            queuedDirection = Direction.WEST;
            transform.rotation = Quaternion.Euler(new Vector3(0,-90,0)); //go left
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            queuedDirection = Direction.EAST;
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); //go right
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            queuedDirection = Direction.EAST;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); //north
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            queuedDirection = Direction.EAST;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); //go south
        }
        
    }

}
