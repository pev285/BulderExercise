
using UnityEngine;




public class InputController : MonoBehaviour {

    [SerializeField]
    private Transform myCamera;
    [SerializeField]
    private float cameraRotationSpeed = 1;
    private float cameraCurrentAngle = 0;
    private float maxCameraAngle = 80;
    private float minCameraAngle = -80;


    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private float moveMinSpeed = 1f;
    [SerializeField]
    private float moveMaxSpeed = 5f;


    [ReadOnly]
    [SerializeField]
    private Vector3 currentMoveAmount = Vector3.zero;
    //private float currentForwardSpeed = 0f;
    //private float currentSidewardSpeed = 0f;
    private readonly float sqrt2 = 1.0f / Mathf.Sqrt(2);

    [ReadOnly]
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


        currentRotationSpeed = horizontalMouseAxis * rotationSpeed / Time.deltaTime;


        currentMoveAmount = verticalAxis * transform.forward + horizontalAxis * transform.right;
        if (verticalAxis != 0 && horizontalAxis != 0)
        {
            currentMoveAmount *= sqrt2;
        }


        currentMoveAmount *= moveMinSpeed;



        cameraCurrentAngle -= verticlMouseAxis * cameraRotationSpeed;
        cameraCurrentAngle = Mathf.Clamp(cameraCurrentAngle, minCameraAngle, maxCameraAngle);

	}

    private void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(currentRotationSpeed * Time.deltaTime, transform.up));

        rb.position += currentMoveAmount * Time.deltaTime;

    }

    private void LateUpdate()
    {
        myCamera.localRotation = Quaternion.Euler(cameraCurrentAngle, 0, 0);
        
    }
}



