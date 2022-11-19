using UnityEngine;
using UnityEngine.Events;

namespace Company
{
    public enum ResourceType { BLUE, GREEN, RED, }
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceType _type;
        public ResourceType type { get { return _type; } }

        [SerializeField] float _lerpTime = 0.2f;
        private float _animationTime = 0;

        Vector3 _startPos;
        Vector3 _endPos;

        private bool _animationEnded;
        public UnityEvent onAnimationEnd;

        private void FixedUpdate()
        {
            Animate();
        }

        void Animate()
        {
            if (!_animationEnded)
            {
                _animationTime += Time.deltaTime * (1 / _lerpTime);

                var pos = Vector3.Lerp(_startPos, _endPos, _animationTime);
                transform.localPosition = pos;
            }

            if (_animationTime >= 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                _animationEnded = true;
                onAnimationEnd?.Invoke();
                _animationTime = 0;
            }
        }

        public void StartAnimate(Vector3 target)
        {
            _endPos = target;
            _startPos = transform.localPosition;
            _animationEnded = false;
        }
    }
}
