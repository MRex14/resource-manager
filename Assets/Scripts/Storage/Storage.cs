using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Company
{
    enum SortingAxis { XY, YZ, negativeXY, negativeYZ}
    public abstract class Storage : MonoBehaviour
    {
        [SerializeField] private string _name;
        public string name { get { return _name; } }

        [SerializeField] private int _capacity;
        
        [SerializeField] protected List<Resource> _storage;
        [SerializeField] private Transform _parent;

        [SerializeField] private Vector3Int _maxInAxis;
        [SerializeField] private SortingAxis _sortingAxis;

        //якщо префаб ресурсу має нестандартний розмір
        [SerializeField] private float _scale = 0.6f;

        public void Add(Resource resource)
        {
            _storage.Add(resource);
            resource.transform.parent = _parent;
            resource.StartAnimate(CalculatePosition(_storage.Count - 1));
        }
        public void Remove()
        {
            _storage.RemoveAt(_storage.Count - 1);
        }
        public void Remove(Resource resource)
        {
            _storage.Remove(resource);
        }

        public void RemoveAndDestroy()
        {
            Resource instance = GetLastResource();
            _storage.Remove(instance);
            Destroy(instance.gameObject);
        }

        public Resource GetLastResource()
        {
            if(!IsEmpty())
                return _storage[_storage.Count - 1];
            return null;
        }
        public bool IsFull()
        {
            return _storage.Count == _capacity;
        }
        public bool IsEmpty()
        {
            return _storage.Count == 0;
        }

        private Vector3 CalculatePosition(int order)
        {
            int x = 0, y = 0, z = 0, max;

            if (_sortingAxis == SortingAxis.negativeXY)
            {
                max = _maxInAxis.x;
                x = order % max;
                z = order / max * -1;
            }
            if (_sortingAxis == SortingAxis.XY)
            {
                max = _maxInAxis.x;
                x = order % max;
                z = order / max;
            }
            if (_sortingAxis == SortingAxis.YZ)
            {
                max = _maxInAxis.y;
                y = order % max;
                z = order / max;
            }
            if (_sortingAxis == SortingAxis.negativeYZ)
            {
                max = _maxInAxis.y;
                y = order % max;
                z = order / max * -1;
            }
            return new Vector3(x, y, z) * _scale;
        }

        public void ReCalculatePosition()
        {
            int i = 0;
            foreach (var res in _storage.ToList())
            {
                res.transform.localPosition = CalculatePosition(i++);
            }
        }
    }
}
