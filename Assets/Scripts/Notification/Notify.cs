using TMPro;
using UnityEngine;

namespace Company
{
    public class Notify : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        [SerializeField] private float _speed;
        [SerializeField] private float _delay;

        private Counter _counter = new Counter();
        public TextMeshProUGUI textMesh;


        private void Update()
        {
            if (_counter.Play(_lifetime) >= _lifetime)
            {
                NotifyManager.instance._notifies.Remove(this);
                Destroy(gameObject);
            }
            if (_counter.GetTime() >= _delay)
            {
                transform.localPosition += new Vector3(0, _counter.GetTime() * _speed, 0);
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - _counter.GetTime() / _lifetime);
            }
        }
    }
}
