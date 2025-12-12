using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    // item prefabList
    public GameObject[] itemPrefabs;

    // Object Pool Dictionary
    private Dictionary<GameObject, List<GameObject>> poolDictionary = new Dictionary<GameObject, List<GameObject>>();

    private List<GameObject> inactiveItemsCache = new List<GameObject>();
    private void Awake()
    {
        // single instace
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (GameObject prefab in itemPrefabs)
        {
            if (!poolDictionary.ContainsKey(prefab))
            {
                poolDictionary[prefab] = new List<GameObject>();

                // Create Initial pool
                for (int i = 0; i < 10; i++) // every object create 10 copy
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    poolDictionary[prefab].Add(obj);
                }
            }
        }
    }

    

    public GameObject GetItem()
    {
        // Clear cache list
        inactiveItemsCache.Clear();

        // Add inactive object to List
        foreach (var pool in poolDictionary.Values)
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    inactiveItemsCache.Add(obj);
                }
            }
        }

        // If have inacitve object choose one of them
        if (inactiveItemsCache.Count > 0)
        {
            GameObject selectedItem = inactiveItemsCache[Random.Range(0, inactiveItemsCache.Count)];
            return selectedItem;
        }

        // If all object activate, create new one
        List<GameObject> prefabKeys = new List<GameObject>(poolDictionary.Keys);
        GameObject randomPrefab = prefabKeys[Random.Range(0, prefabKeys.Count)];
        GameObject newObj = Instantiate(randomPrefab);
        newObj.SetActive(false);
        poolDictionary[randomPrefab].Add(newObj);
        return newObj;
    }

    // Return (Deactive) object
    public void ReturnItem(GameObject obj)
    {
        obj.SetActive(false);
    }
}
