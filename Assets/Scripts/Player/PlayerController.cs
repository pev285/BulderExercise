
using BuilderGame.DataBus;
using UnityEngine;


namespace BuilderGame.Player
{
    public class PlayerController : MonoBehaviour
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


        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            CommandKeeper.OnPlayerMoveCommand += OnPlayerMoveCommand;
            CommandKeeper.OnPlayerRotateCommand += OnPlayerRotateCommand;

            CommandKeeper.GetPlayerPosition += GetPlayerPosition;
            CommandKeeper.GetPlayerForward += GetPlayerForward;
            CommandKeeper.GetPlayerRotationAngle += GetPlayerRotationAngle;
        }

        #region DataBus methods 

        private float GetPlayerRotationAngle()
        {
            float angle = 0.0F;
            Vector3 axis = Vector3.zero;
            transform.rotation.ToAngleAxis(out angle, out axis);
            return angle;
        }

        private Vector3 GetPlayerPosition()
        {
            return transform.position;
        }

        private Vector3 GetPlayerForward()
        {
            return transform.forward;
        }

        private void OnPlayerMoveCommand(float horizontalAxis, float verticalAxis) 
        {
            CalculateMovement(horizontalAxis, verticalAxis);
        }

        private void OnPlayerRotateCommand(float horizontalMouseAxis, float verticalMouseAxis)
        {
            CalculateHorizontalRotation(horizontalMouseAxis);
            CalculateCameraVerticalRotation(verticalMouseAxis);
        }

        #endregion

        #region Movement calculations

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

        #endregion

        #region Updates

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

        #endregion

    } // End of class ///



} // namespace ///

