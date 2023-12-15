using UnityEngine;

namespace Dodger.Domain.Spawning
{
    public class Spawner<T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
        private Vector3 _spawnPosition;
        private Quaternion _spawnRotation;

        public Spawner()
        {
            _spawnPosition = Vector3.zero;
            _spawnRotation = Quaternion.identity;
        }

        public void SetPrefab(T prefab)
        {
            _prefab = prefab;
        }

        public void SetParent(Transform parent)
        {
            _parent = parent;
        }

        public void SetSpawnPosition(Vector3 position)
        {
            _spawnPosition = position;
        }

        public void SetSpawnRotation(Quaternion rotation)
        {
            _spawnRotation = rotation;
        }

        public T Spawn()
        {
            return Object.Instantiate(_prefab, _spawnPosition, _spawnRotation, _parent);
        }
    }
}
