using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    public List<GameObject> _pooledObjects;
    
    [SerializeField] private T prefab;
    [SerializeField] private int amountToPool;
    [SerializeField] private bool shouldExpand = true;
    [SerializeField] private GameObject prefabContainter;
    public static GenericObjectPool<T> Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _pooledObjects = new List<GameObject>();

        for (var i = 0; i < amountToPool; i++)
        {
            var obj = Instantiate(prefab, prefabContainter.transform);
            obj.gameObject.SetActive(false);
            _pooledObjects.Add(obj.gameObject);
        }
      
    }

    public GameObject GetPooledObject()
    {
        for (var i = 0; i < _pooledObjects.Count; i++)
            if (!_pooledObjects[i].activeInHierarchy)
                return _pooledObjects[i];


        if (shouldExpand)
        {
            var obj = Instantiate(prefab, prefabContainter.transform);
            obj.gameObject.SetActive(false);
            _pooledObjects.Add(obj.gameObject);
            return obj.gameObject;
        }

        return null;
    }
}
