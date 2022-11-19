using UnityEngine;

namespace Company
{
    public class ConsumedStorage : Storage
    {
        [SerializeField] private ResourceType _type;
        public ResourceType type { get { return _type; } }
    }
}
