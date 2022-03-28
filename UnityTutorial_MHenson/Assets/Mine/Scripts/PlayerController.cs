using System.Collections;
using System.Collections.Generic;
using UnityEngine; //uses unity's systems
using UnityEngine.InputSystem; //enables player movement
using TMPro; //uses text mesh pro to edit text

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody; //creates variable for applying force to rigidbody of player

    private float moveX;
    private float moveY;
    private int pickupCounter = 0; //integer value of how many pickups have been collected

    public float speed = 0; //how fast player moves
    public TextMeshProUGUI countText; //how many "points" player has
    public GameObject winTextObect; //to enable win text

    // Start is called before the first frame update
    void Start()
    {
        winTextObect.SetActive(false); //ensures text is false at beginning
        pickupCounter = 0; //sets initial count to 0
        rigidBody = GetComponent<Rigidbody>(); //grabs players rigidbody

        SetCountText(); //counts text and applies it
    }

    void FixedUpdate() //called before physics calculation so that everything flows neatly
    {
        Vector3 movement = new Vector3(moveX, 0.0f, moveY); //creates a vector3 using values grabbed from vector2 and a value of 0 for z axis

        rigidBody.AddForce(movement * speed); //applies movement force to player rigidbody
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); //grabs players location and sets it equal to a vector so force can be applied to it
        moveX = movementVector.x; //sets global move x to equal the movement vector of x
        moveY = movementVector.y; //sets global move y to equal the movement vector of y
    }

    private void SetCountText()
    {
        countText.text = "Counter: " + pickupCounter.ToString(); //sets text to inform player of how many pickups have been collected

        if (pickupCounter >= 12) //lower than 12 is for debugging and it should be 12 at the end
        {
            winTextObect.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) //enables player collision to trigger objects
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            pickupCounter = pickupCounter + 1; //adds one to pickup counter
            //can also be written pickupCounter++; or pickupCounter += 1;
            SetCountText(); //counts text and applies it
            other.gameObject.SetActive(false); //just for debugging at first
        }
    }
}
