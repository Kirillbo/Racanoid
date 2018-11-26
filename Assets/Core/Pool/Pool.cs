using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool
{

    private object _originalObj;

    private Queue<GameObject> _cashedStack;

    public Transform CommonTransform;

    private int _idPool;

        
    //Pool for object on Scene
    public Pool(int id, int amount, Object prefabe, Transform parent = null)
    {
        _cashedStack = new Queue<GameObject>();
        CommonTransform = new GameObject(Enum.GetName(typeof(PoolType), id) + "_POOL").transform;
        _idPool = id;
        _originalObj = prefabe;

        for (int i = 0; i < amount; i++)
        {
            GameObject go = Object.Instantiate(prefabe, Vector3.zero, Quaternion.identity, CommonTransform) as GameObject;
            _cashedStack.Enqueue(go);
            go.SetActive(false);

            IPoollable ipoolable = go.GetComponent<IPoollable>();
            if(ipoolable != null) ipoolable.Init();
        }

        if (parent != null)
        {
            CommonTransform.SetParent(parent);
        }
    }


    public bool Contains(GameObject obj)
    {
        return _cashedStack.Contains(obj);
    }

 
    public Queue<GameObject> GetCollection()
    {
        return _cashedStack;
    }


    public void AddObject(GameObject obj, bool commonTransform)
    {
        _cashedStack.Enqueue(obj);

        if (commonTransform)
        {
            obj.transform.SetParent(CommonTransform);
        }
    }

    public GameObject Get()
    {
        if (_cashedStack.Count < 1)
        {
            Debug.LogFormat("Stack {0} empty.", _idPool);
            return null;
        }
        
        GameObject b = (GameObject)_cashedStack.Peek();
        return b;
    }

    public GameObject ReSpawn()
    {
        if (_cashedStack.Count < 1)
        {
            Debug.LogFormat("Stack {0} empty.", _idPool);
            return null;
        }
        GameObject b = (GameObject)_cashedStack.Dequeue();
        return b;
    }

    
    public GameObject OriginalPrefabe()
    {
        return (GameObject)_originalObj;
    }

}
