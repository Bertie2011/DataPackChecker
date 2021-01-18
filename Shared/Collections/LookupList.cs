using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DataPackChecker.Shared.Collections {
    public class LookupList<K, V> : ICollection<V> where V : HasKey<K> {

        private Dictionary<K, V> Data { get; }
        
        public V this[K key] { get => Data[key]; set => Data[key] = value; }

        public int Count => Data.Count;

        public bool IsReadOnly => false;

        public LookupList() {
            Data = new Dictionary<K, V>();
        }
        
        public LookupList(LookupList<K, V> list) {
            Data = new Dictionary<K, V>(list.Data);
        }

        public LookupList(int capacity) {
            Data = new Dictionary<K, V>(capacity);
        }

        public LookupList(LookupList<K, V> list, IEqualityComparer<K> comparer) {
            Data = new Dictionary<K, V>(list.Data, comparer);
        }

        public LookupList(int capacity, IEqualityComparer<K> comparer) {
            Data = new Dictionary<K, V>(capacity, comparer);
        }

        public LookupList(IEqualityComparer<K> comparer) {
            Data = new Dictionary<K, V>(comparer);
        }

        public void Add(V item) {
            Data.Add(item.Key, item);
        }

        public void Clear() {
            Data.Clear();
        }

        public bool Contains(V item) {
            return Data.ContainsKey(item.Key);
        }

        public bool ContainsKey(K key) {
            return Data.ContainsKey(key);
        }

        public void CopyTo(V[] array, int arrayIndex) {
            foreach (var val in this) {
                array[arrayIndex] = val;
                arrayIndex++;
            }
        }

        public IEnumerator<V> GetEnumerator() {
            return Data.Values.GetEnumerator();
        }

        public bool Remove(V item) {
            return Data.Remove(item.Key);
        }

        public bool Remove(K key) {
            return Data.Remove(key);
        }

        public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value) {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Data.Values.GetEnumerator();
        }
    }
}
