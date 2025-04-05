using UnityEngine;

namespace RobFreelookCamera
{
    public class FreelookMove : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private float m_playerMoveSpeed;

        [Header("Camera")]
        [SerializeField] private Transform m_camera;
        [SerializeField] private Vector3 m_cameraOffset = new Vector3(0, 5f, -10f);
        [SerializeField] private float m_cameraSmoothTime = 0.1f;
        [SerializeField] private float m_lookSensitivity = 100f;

        [Header("Camera Min Max Clamp")]
        [Tooltip("X = Min, Y = Max")]
        [SerializeField] private Vector2 m_clampMinMax;

        private Vector3 m_cameraVelocity;
        private float m_currentPitch = 0f;
        private float m_currentYaw = 0f;

        private void Start()
        {
            SetCursor();
        }

        void Update()
        {
            Move();
        }
        private void LateUpdate()
        {
            //Late Update for the camera
            Look();
            CameraFollow();
        }
        private void SetCursor() 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Move() 
        {
            Vector3 move = Vector3.zero;

            move.x = Input.GetAxis("Horizontal");
            move.z = Input.GetAxis("Vertical");

            move = m_camera.TransformDirection(move);

            move.y = 0; //cancel out the camera tilt
            //make the direction lengh 1 again and multi by the speed 
            move = move.normalized * m_playerMoveSpeed * Time.deltaTime;

            transform.position += move;

            if (move.sqrMagnitude != 0)
            {
                transform.forward = move.normalized;
            }
        }
        private void Look()
        {
            float mouseX = Input.GetAxis("Mouse X") * m_lookSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_lookSensitivity * Time.deltaTime;

            m_currentYaw += mouseX;
            m_currentPitch -= mouseY;
            m_currentPitch = Mathf.Clamp(m_currentPitch, m_clampMinMax.x, m_clampMinMax.y);
        }
        private void CameraFollow() 
        {
            Quaternion targetRot = Quaternion.Euler(m_currentPitch, m_currentYaw, 0f);

            // Calculate desired camera position based on rotation and offset
            Vector3 desiredPos = transform.position + targetRot * m_cameraOffset;

            // Smooth position
            m_camera.position = Vector3.SmoothDamp(m_camera.position, desiredPos, ref m_cameraVelocity, m_cameraSmoothTime);

            // Look at the player
            m_camera.rotation = Quaternion.Slerp(m_camera.rotation, targetRot, Time.deltaTime * 10f);
        }

    }
}

