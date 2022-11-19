using UnityEngine;

namespace Company
{
    public class CameraController : MonoBehaviour
    {
        public Transform _target;
        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}
