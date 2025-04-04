using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float m_lookSensitivty;
    [SerializeField] private Transform m_cameraParent;

    private Vector3 m_rotationAxis;

    public Vector3 Forward { get { return transform.forward; } }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_rotationAxis = Vector3.up;
    }
    void Update()
    {
        transform.RotateAround(m_cameraParent.position, m_rotationAxis, (m_lookSensitivty * Time.deltaTime * Input.GetAxis("Mouse X")));

        m_cameraParent.RotateAround(m_cameraParent.position, transform.right, (m_lookSensitivty * Time.deltaTime * Input.GetAxis("Mouse Y")));

        Vector3 euler = m_cameraParent.localEulerAngles;
        float eulerX = euler.x;

        if (eulerX > 180f) eulerX -= 360f;
        eulerX = Mathf.Clamp(eulerX, -50, 50);

        euler.x = eulerX;
        m_cameraParent.localRotation = Quaternion.Euler(euler);

    }
}
