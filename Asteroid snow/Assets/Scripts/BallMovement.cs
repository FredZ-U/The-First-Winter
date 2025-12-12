using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    [HideInInspector]
    public Vector3 targetVelocity = new Vector3(0f, 0f, 25f);
    private Rigidbody rb;

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    public float dragSpeed = 0.01f;
    public bool controllingPlayer = true;//Kevin added this
    [HideInInspector]
    public bool startGame;//I also added this for the button script to decide when to start game


    private Vector3 initialMousePosition;
    private Vector3 initialBallPosition;
    public float moveSpeed = 0.01f;

    public float maxSpeed = 50f;
    void Start()
    {
        mainCamera = Camera.main;

        targetVelocity = new Vector3(0f, 0f, 25f);
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = rotationSpeed;
        //rb.velocity = new Vector3(0, 0, transform.forward.z * speed);
    }

    void FixedUpdate()
    {
        if(startGame)//threw all of the physics in this statement to allow the snowman to animate.
        {
            //if it is dragging, update ball position
            if (isDragging && controllingPlayer)//Kevin added this extra conditional
            {
                DragBall();
                Debug.Log("dragging");
            }
            else
            {
                float verticalVelocity = rb.velocity.y;

                rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
                Debug.Log(rb.velocity);
            }
            //rb.angularVelocity = transform.right * rotationSpeed;
        }

    }

    void Update()
    {
        if (startGame)//same as fixed update. Wanna allow for animation b4 game starts
        {
            // Detect Mouse button press and release
            if (Input.GetMouseButtonDown(0))
            {
                CheckIfClickedOnBall();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
        
    }

    void CheckIfClickedOnBall()
    {
        // Raycast from mouse positon
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Detect ray hit the bal or not
        if (Physics.Raycast(ray, out hit))
        {
            // 判detect ball is hit this object or not
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;

                // caculate offset value
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
                offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);
            }
        }
    }


    void DragBall()
    {

        // Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

        // convert screen position to world position
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;

        // Caculate target position and direction from the ball
        Vector3 direction = (targetPosition - transform.position)*4;
        float distance = direction.magnitude;
        // limit max speed
        if (distance > maxSpeed)
        {
            direction = direction.normalized * maxSpeed;
        }

        // update ball x position(left and right)
        //transform.position += new Vector3(direction.x, 0, 0);
        rb.velocity = new Vector3(direction.x, rb.velocity.y, targetVelocity.z);
        Debug.Log(rb.velocity);
    }

    //Kevin Added everything below this
    public IEnumerator ControllerToggle()
    {
        controllingPlayer = false;
        yield return new WaitForSeconds(.5f);
        controllingPlayer = true;
    }
    
    public void StartControlToggle()
    {
        StartCoroutine(ControllerToggle());
    }
}

