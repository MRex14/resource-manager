using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Company
{
    public class NotifyManager : MonoBehaviour
    {
        public static NotifyManager instance;

        [SerializeField] Notify _prefab;

        public ConsumedStorage[] _consumedStorages;
        public ProducedStorage[] _producedStorages;
        public List<Notify> _notifies;


        public  bool[] _consumedStoragesIsNotified;
        public  bool[] _producedStoragesIsNotified;


        private void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(instance.gameObject);
        }

        private void Start()
        {
            _consumedStorages = FindObjectsOfType<ConsumedStorage>();
            _producedStorages = FindObjectsOfType<ProducedStorage>();

            _consumedStoragesIsNotified = new bool[_consumedStorages.Length];
            _producedStoragesIsNotified = new bool[_producedStorages.Length];
        }

        private void Update()
        {
            for(int i = 0; i < _consumedStorages.Length; i++)
            {
                if (_consumedStorages[i].IsEmpty() && _consumedStoragesIsNotified[i] == false)
                {
                    CreateNotify($"{_consumedStorages[i].name} consumed storage is empty!");
                    _consumedStoragesIsNotified[i] = true;
                }
                if (!_consumedStorages[i].IsEmpty() && _consumedStoragesIsNotified[i] == true)
                    _consumedStoragesIsNotified[i] = false;
            }

            for (int i = 0; i < _producedStorages.Length; i++)
            {
                if (_producedStorages[i].IsFull() && _producedStoragesIsNotified[i] == false)
                {
                    CreateNotify($"{_producedStorages[i].name} produced storage is full!");
                    _producedStoragesIsNotified[i] = true;
                }
                if (!_producedStorages[i].IsFull() && _producedStoragesIsNotified[i] == true)
                    _producedStoragesIsNotified[i] = false;
            }
        }


        public void CreateNotify(string text)
        {
            print(text);
            var notify = Instantiate(_prefab, transform);
            notify.transform.localPosition = new Vector3(0, -100 * _notifies.Count, 0);
            _notifies.Add(notify);

            notify.GetComponent<TextMeshProUGUI>().text = text;
        }
    }
}
