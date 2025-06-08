using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint maxSize = 15; // Numero massimo di oggetti per tipo
    [SerializeField] private List<PooledObject> pooledPrefabs; // Lista di prefabs diversi
    
    private Dictionary<PooledObject, Stack<PooledObject>> poolDictionary = new Dictionary<PooledObject, Stack<PooledObject>>();

    private void Start()
    {
        SetUpPool();
    }

    private void SetUpPool()
    {
        foreach (var prefab in pooledPrefabs)
        {
            poolDictionary[prefab] = new Stack<PooledObject>();

            for (int i = 0; i < maxSize; i++)
            {
                PooledObject instance = Instantiate(prefab);
                instance.Pool = this;
                instance.PrefabReference = prefab; // Memorizza il prefab originale
                instance.gameObject.SetActive(false);
                poolDictionary[prefab].Push(instance);
            }
        }
    }

    public PooledObject GetPooledObject(PooledObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("Il prefab richiesto non esiste nel pool!\n");
            return null;
        }

        // Se non ci sono più oggetti in pool, ne creiamo uno nuovo
        if (poolDictionary[prefab].Count == 0)
        {
            PooledObject newInstance = Instantiate(prefab);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject instance = poolDictionary[prefab].Pop();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        if (!poolDictionary.ContainsKey(pooledObject.PrefabReference))
        {
            Destroy(pooledObject.gameObject);
            return;
        }

        pooledObject.gameObject.SetActive(false);
        poolDictionary[pooledObject.PrefabReference].Push(pooledObject);
    }
}