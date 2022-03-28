using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    public bool IsPaused = false, //bool for the "game" to figure out if it's paused or not
                UseCursorLock; //bool to determine if cursor is locked or not

    public string ButtonToTogglePause, //button to configure for pausing the process
                  canvasIndex, //declares current rendered canvas
                  StartIndex; //string declaring which canvas is rendered first upon pausing
    #endregion
    #region Menu types
    public enum MenuType
    {
        unity3D, unity2D
    }

    public MenuType TypeOfMenu;
    #endregion

    [SerializeField] Texture2D cursor;

    // Use this for initialization
    void Start ()
    {
        if (TypeOfMenu == MenuType.unity2D)
        {

        }
        canvasIndex = StartIndex; //set currently rendered canvas to = the starting canvas
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown(ButtonToTogglePause) && IsPaused == false)
        {
            IsPaused = true;
            
        }
        CheckPause();
    }

    public void CheckPause()
    {
        //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked; //locks cursor to center
        Cursor.visible = false; //hides cursor from view

        if (IsPaused)
        {
            Time.timeScale = 0.0000000001f; //sets "game" time down ridiculously low to simulate a pause

            AudioListener.pause = true; //pauses all audio in the program when paused

            if (UseCursorLock) //allows the use of cursor again
            {
                Cursor.lockState = CursorLockMode.None; //unlocks from center of screen
                Cursor.visible = true; //shows cursor for user to use
            }
        }

        if (!IsPaused)
        {
            Time.timeScale = 1; //sets back to normal time
            AudioListener.pause = false; //starts playing audio once again
        }
    }
}
