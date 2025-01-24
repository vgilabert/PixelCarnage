using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    [System.Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<SerializedDictionaryItem<TKey, TValue>> items = new();

        public void OnBeforeSerialize()
        {
            this.Clear();
            foreach (var pair in this)
            {
                items.Add(new SerializedDictionaryItem<TKey, TValue> { key = pair.Key, value = pair.Value });
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                this[items[i].key] = items[i].value;
            }
        }
    }
    
    [System.Serializable]
    public class SerializedDictionaryItem<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}