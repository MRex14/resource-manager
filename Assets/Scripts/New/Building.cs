using System.Linq;
using UnityEngine;

namespace Company
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private ConsumedStorage[] _consumedStorage;
        [SerializeField] private ProducedStorage _producedStorage;

        [SerializeField] private float _productionSpeed;
        [SerializeField] private Resource _resourcePrefab;

        [SerializeField] Transform _spawnResourcePosition;

        private Counter _counter = new Counter();

        private void Start()
        {
            _counter.onFinished.AddListener(Production);
        }

        private void Update()
        {
            if (CanProduced())
            {
                _counter.Play(_productionSpeed);
            }
        }

        bool CanProduced()
        {
            if (_producedStorage.IsFull())
                return false;

            foreach(var storage in _consumedStorage)
            {
                if(storage.IsEmpty() )
                    return false;
            }
            return true;
        }

        void Production()
        {
            foreach (var item in _consumedStorage)
            {
                var res = item.GetLastResource();
                res.transform.parent = transform;
                res.StartAnimate(Vector3.zero);
                res.onAnimationEnd.AddListener(item.RemoveAndDestroy);
            }

            var instance = Instantiate(_resourcePrefab);
            instance.transform.position = _spawnResourcePosition.position;
            _producedStorage.Add(instance);
        }
    }
}
