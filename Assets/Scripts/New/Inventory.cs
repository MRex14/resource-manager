using System.Linq;
using UnityEngine;

namespace Company
{
    public class Inventory : Storage
    {
        [SerializeField] float _pickUpRate;
        private Counter _counter = new Counter();

        void PickUp(ProducedStorage storage)
        {
            Add(storage.GetLastResource());
            storage.Remove();
        }

        void PutDown(ConsumedStorage storage)
        {
            foreach (var res in _storage.ToList())
            {
                if (res.type == storage.type)
                {
                    storage.Add(res);
                    Remove(res);
                    ReCalculatePosition();
                    return;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ProducedStorage producedStorage))
                if (!producedStorage.IsEmpty() && !IsFull() && _counter.Play(_pickUpRate) >= _pickUpRate)
                    PickUp(producedStorage);

            if (other.gameObject.TryGetComponent(out ConsumedStorage consumedStorage))
                if (!consumedStorage.IsFull() && !IsEmpty() && _counter.Play(_pickUpRate) >= _pickUpRate)
                    PutDown(consumedStorage);
        }
    }
}
