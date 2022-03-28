using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region mouse cam variables
    [HideInInspector] public Vector2 rotating;
    private float sensitivity = .1f;
    private Vector3 deltaMove;
    private float speed = .5f;
    public GameObject player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        rotating.x += Input.GetAxis("Mouse X") * sensitivity;
        rotating.y += Input.GetAxis("Mouse Y") * sensitivity;
        //player.transform.localRotation = Quaternion.AngleAxis(rotating.x, Vector3.up);
        //player.transform.localRotation = Quaternion.AngleAxis(-rotating.y, Vector3.right);
        //player.transform.localRotation = Quaternion.Euler(-rotating.y, rotating.x, 0);
        transform.Rotate(-rotating.y, rotating.x, 0);

        deltaMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        player.transform.Translate(deltaMove);
        /*Plane rotateAroundY = new Plane(Vector3.up, transform.position);
        //Plane rotateAroundX = new Plane(Vector3.right, transform.position);
        //Plane rotateAroundZ = new Plane(Vector3.forward, transform.position);
        Ray ray = shipCam.ScreenPointToRay(Input.mousePosition);
        float hitDistance;

        if (rotateAroundY.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation((targetPoint) - transform.position);
            //Debug.Log("Target Point: " + targetPoint);
            //Debug.Log("Target Rotation: " + targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
        }*/
    }
}
