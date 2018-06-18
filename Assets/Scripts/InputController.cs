using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

public class InputController : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 50f;
    [SerializeField]
    private float moveMinSpeed = 1f;
    [SerializeField]
    private float moveMaxSpeed = 5f;



    [SerializeField]
    private Vector3 currentMoveAmount = Vector3.zero;
    private float currentForwardSpeed = 0f;
    private float currentSidewardSpeed = 0f;
    private readonly float sqrt2 = 1.0f / Mathf.Sqrt(2);


    [SerializeField]
    private float currentRotationSpeed = 0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalMouseAxis = Input.GetAxis("Mouse X");
        float verticlMouseAxis = Input.GetAxis("Mouse Y");
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");


        currentRotationSpeed = horizontalMouseAxis * rotationSpeed;


        currentMoveAmount = verticalAxis * transform.forward + horizontalAxis * transform.right;
        if (verticalAxis != 0 && horizontalAxis != 0)
        {
            currentMoveAmount *= sqrt2;
        }


        currentMoveAmount *= moveMinSpeed;

	}

    private void FixedUpdate()
    {
        rb.rotation = rb.rotation * Quaternion.AngleAxis(currentRotationSpeed * Time.deltaTime, transform.up);

        rb.position += currentMoveAmount * Time.deltaTime;
    }
}



