using UnityEngine;

namespace RobCamera
{
    public class KeyboardMove : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        void Update()
        {
            Vector3 move = Vector3.zero;

            move.x = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
            move.z = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;

            move = Vector3.ClampMagnitude(move, m_speed);

            transform.Translate(move);
        }
    }
}
