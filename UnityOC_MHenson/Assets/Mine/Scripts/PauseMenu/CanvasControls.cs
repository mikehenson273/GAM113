using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControls : MonoBehaviour
{
    #region Variables
    PauseMenu PauseControl; //References Pause Menu controls
    public string MyIndex; //defines index of currently loaded canvas
    Canvas MyCanvas; //defines what the canvas is on the object this code is attached
    #endregion

    // Use this for initialization
    void Start ()
    {
        MyCanvas = gameObject.GetComponent<Canvas>(); //sets canvas
        PauseControl = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (PauseControl.IsPaused) //the "game" is paused
        {
            if (PauseControl.canvasIndex == MyIndex) //if canvas index is same as ours
            {
                MyCanvas.enabled = true; //render the canvas and all associated components
            }

            if (PauseControl.canvasIndex != MyIndex) //if paused but not the same index
            {
                MyCanvas.enabled = false; //do NOT rend this canvas
            }
        }

        if (!PauseControl.IsPaused) //if game is NOT paused do NOT render canvas
        {
            MyCanvas.enabled = false;
        }
	}
}
