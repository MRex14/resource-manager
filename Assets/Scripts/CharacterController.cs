using UnityEngine;

namespace Company
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody _rigidbody;

        private Vector3 _input;

        [SerializeField] Joystick _joystick;
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

    
        void Update()
        {
            _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Look()
        {
            if(_input != Vector3.zero)
            {
                Vector3 relative = (transform.position + _input) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

                transform.rotation = rotation;
            }
        }
        void Move()
        {
            _rigidbody.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
        }
    }
}