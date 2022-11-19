using UnityEngine;
using UnityEngine.Events;

namespace Company
{
    public class Counter
    {
        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onProgress = new UnityEvent();
        public UnityEvent onFinished = new UnityEvent();

        private float _curTime;

        public float Play(float time)
        {
            if (_curTime == 0f)
                onStart?.Invoke();
            if (_curTime < time)
            {
                _curTime += Time.deltaTime;
                onProgress?.Invoke();
            }
            else
            {
                onFinished?.Invoke();
                _curTime = 0f;
            }

            return _curTime;
        }

        public float GetTime()
        {
            return _curTime;
        }
    }
}
