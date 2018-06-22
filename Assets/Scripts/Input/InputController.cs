
using UnityEngine;


namespace BuilderGame.InputReading
{
    public class InputController : MonoBehaviour
    {

        [SerializeField]
        private Transform myCamera;
        [SerializeField]
        private float verticalRotationSpeed = 1;
        private float currentCameraAngle = 0;
        [SerializeField]
        private float maxCameraAngle = 80;
        [SerializeField]
        private float minCameraAngle = -80;

        [Space(20)]

        [SerializeField]
        private float horizontalRotationSpeed = 2f;
        private float currentRotationSpeed = 0f;


        [Space(20)]
        [SerializeField]
        private float minMoveSpeed = 1f;
        [SerializeField]
        private float maxMoveSpeed = 5f;
        [SerializeField]
        private float moveAcceleration = 1f;
        [ReadOnly]
        [SerializeField]
        private float currentMoveSpeed = 0f;

        [ReadOnly]
        [SerializeField]
        private Vector3 currentMoveDirection = Vector3.zero;
        //private readonly float sqrt2 = 1.0f / Mathf.Sqrt(2); // Use coeff if moving along two axis.


        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float horizontalMouseAxis = Input.GetAxis("Mouse X");
            float verticlMouseAxis = Input.GetAxis("Mouse Y");
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            CalculateHorizontalRotation(horizontalMouseAxis);

            CalculateMovement(horizontalAxis, verticalAxis);

            CalculateCameraVerticalRotation(verticlMouseAxis);

        }

        private void CalculateCameraVerticalRotation(float verticlMouseAxis)
        {
            currentCameraAngle -= verticlMouseAxis * verticalRotationSpeed;
            currentCameraAngle = Mathf.Clamp(currentCameraAngle, minCameraAngle, maxCameraAngle);
        }

        private void CalculateMovement(float horizontalAxis, float verticalAxis)
        {
            if (verticalAxis != 0 || horizontalAxis != 0)
            {
                currentMoveDirection = verticalAxis * transform.forward + horizontalAxis * transform.right;
                currentMoveDirection.Normalize();
                /*            if (verticalAxis != 0 && horizontalAxis != 0)
                            {
                                currentMoveDirection *= sqrt2;
                            }
                            */

                if (currentMoveSpeed == 0)
                {
                    currentMoveSpeed = minMoveSpeed;
                }
                else
                {
                    currentMoveSpeed += moveAcceleration * Time.deltaTime;
                }
                if (currentMoveSpeed > maxMoveSpeed)
                {
                    currentMoveSpeed = maxMoveSpeed;
                }
            }
            else
            {
                currentMoveSpeed -= moveAcceleration * Time.deltaTime;
                if (currentMoveSpeed < minMoveSpeed)
                {
                    currentMoveSpeed = 0;
                }
            }
        }

        private void CalculateHorizontalRotation(float horizontalMouseAxis)
        {
            currentRotationSpeed = horizontalMouseAxis * horizontalRotationSpeed / Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (currentRotationSpeed != 0)
            {
                rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(currentRotationSpeed * Time.deltaTime, transform.up));
            }

            if (currentMoveSpeed != 0)
            {
                rb.MovePosition(rb.position + currentMoveDirection * currentMoveSpeed * Time.deltaTime);
            }
        } // FixedUpdate() ////

        private void LateUpdate()
        {
            myCamera.localRotation = Quaternion.Euler(currentCameraAngle, 0, 0);
        }



    } // End of class ///



} // namespace ///

