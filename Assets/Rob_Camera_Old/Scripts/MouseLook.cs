using UnityEngine;

namespace RobCamera
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float m_lookSensitivity = 100f;
        [SerializeField] private Transform m_cameraParent;

        [Header("Camera Min Max Clamp")]
        [Tooltip("X = Min, Y = Max")]
        [SerializeField] private Vector2 m_clampMinMax;

        private float m_pitch = 0f;

        public Vector3 Forward => transform.forward;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * m_lookSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_lookSensitivity * Time.deltaTime;

            transform.RotateAround(transform.position,Vector3.up,mouseX);

            m_pitch -= mouseY;
            m_pitch = Mathf.Clamp(m_pitch, m_clampMinMax.x, m_clampMinMax.y);

            m_cameraParent.localRotation = Quaternion.Euler(m_pitch, 0f, 0f);
        }
    }
}
