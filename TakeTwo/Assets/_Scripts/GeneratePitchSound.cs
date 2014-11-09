using UnityEngine;
using System.Collections;

public class GeneratePitchSound : MonoBehaviour {

	// Use this for initialization
    public const float MAX_TIME = .5f;
    private float currentTimeRemaining;

    public const float DEFAULT_PITCH = 1;
    public const float PITCH_CAP = 2;
    public float currentPitch;

    public bool consecutive;

	void Start () {
        currentTimeRemaining = MAX_TIME;
        currentPitch = DEFAULT_PITCH;
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(!consecutive)
        {
            currentTimeRemaining -= Time.deltaTime;
        }
        else
        {
            currentTimeRemaining = MAX_TIME;
            currentPitch += .02f;
            if (currentPitch > PITCH_CAP)
                currentPitch = PITCH_CAP;
            consecutive = false;
        }

        if(currentTimeRemaining < 0)
        {
            currentPitch = DEFAULT_PITCH;
        }
	}

    public void handleSound()
    {
        audio.pitch = currentPitch;
     
        audio.Play();
        consecutive = true;
    }
}
