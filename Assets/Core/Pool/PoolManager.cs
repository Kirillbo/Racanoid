using System;
using System.Linq;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PoolManager : SingltoonBehavior<PoolManager>
{

    public bool DynamicPool;
    
	private Dictionary<int, Pool> _dictGameObject = new Dictionary<int, Pool>();
    private Dictionary<int, IComponent> _dictComponent = new Dictionary<int, IComponent>();
    
    
    
    public Pool CreatePool(PoolType id, int amount, GameObject prefabe, Transform parent = null)
    {
        Pool pool;
        int IntID = (int) id;
        var newParent = parent ? transform : parent;
        
            
        if(!_dictGameObject.ContainsKey(IntID))
        {
            pool = new Pool(IntID, amount, prefabe, newParent);
            _dictGameObject.Add(IntID, pool);
            return pool;
        }

        Debug.Log(id + " pool already exist");
        return null;
    }

    public void DestroyPool(PoolType id)
    {
        int IntID = (int) id;
           
        if(_dictGameObject.ContainsKey(IntID))
        {
            _dictGameObject.Remove(IntID);
            return;
        }

        Debug.Log(id + " pool already exist");
    }
    
    public T AddComponent<T>() where T : IComponent,  new()
    {
        var key = typeof(T).GetHashCode();
        
        if (!_dictComponent.ContainsKey(key))
        {
            var needObj = new T();
            _dictComponent.Add(key, needObj);
            return needObj;
        }
        
        Debug.Log(typeof(T) + " this component is registered");
        return (T) _dictComponent[key];
    }

    public void AddComponent(object component)
    {
        var scriptable = component as ScriptableObject;
        if (scriptable != null) Object.Instantiate(scriptable);
        
    }

    
    
    //Get gameObjects 
    public GameObject Get(PoolType id)
    {
        Pool pool;
        
        if(_dictGameObject.TryGetValue((int)id, out  pool))
        {
            return pool.Get();
        }
        
        Debug.Log("Pool is not exist");
        return null;
    }

    public void RemoveComponent<T>()
    {
        var key = typeof(T).GetHashCode();
        if (_dictComponent.ContainsKey(key))
        {
            _dictComponent.Remove(key);
        }
    } 
    
    //Get components
    public T Get<T>() where T : class, IComponent
    {
        var key = typeof(T).GetHashCode();
        IComponent needComponent;

        if (_dictComponent.TryGetValue(key, out needComponent))
        {
            return needComponent as T;
        }
        
        Debug.Log("Component is not find");
        return null;
    }
    
    public bool IsContaince(PoolType id, GameObject obj)
    {
        return _dictGameObject[(int) id].Contains(obj);
    }


    public Queue<GameObject> GetStack(PoolType id)
    {
        if (_dictGameObject.ContainsKey((int) id))
        {
            return _dictGameObject[(int) id].GetCollection();
        }

        Debug.Log("Pool is not exist");
        return null;
    } 

    public GameObject ReSpawn(PoolType id)
    {
        var obj = _dictGameObject[(int) id].ReSpawn();
        if (obj == null)
        {
            Debug.LogFormat("Pool {0} is empty.", id);

            if (DynamicPool)
            {
                obj = Instantiate(_dictGameObject[(int) id].OriginalPrefabe());
                IPoollable IPoollabl = obj.GetComponent<IPoollable>();
                if(IPoollabl != null) IPoollabl.Init();

                Debug.LogFormat("Add one object to {0} pool.", id );
            }
        }

        IPoollable iPoollable = obj.GetComponent<IPoollable>();
        if(iPoollable != null) iPoollable.ReSpawn();
        
        return obj;
    }




#if UNITY_EDITOR
//    public string[] ListPool;
//    
//    void Update()
//    {
//                
//        for (int i = 0; i < _pools.Count; i++)
//        {
//            var key  = _pools.ElementAt(i).Key;
//            var namePool = Enum.GetName(typeof(PoolType), i);
//            var countElementInPool = _pools[i].GetStack().Count;
//            ListPool[i] = String.Concat(namePool, " ", countElementInPool);
//        }
//    }
#endif
 

    public void DeSpawn(PoolType id, GameObject obj, bool commonTransform = true, bool setActive = false)
    {
        Pool pool;

        if(_dictGameObject.TryGetValue((int)id, out pool))
        {
            pool.AddObject(obj, commonTransform);

            var ipoolable = obj.GetComponent<IPoollable>();
            if (ipoolable != null) ipoolable.DeSpawn();

            obj.SetActive(setActive);
        }

        else Debug.LogFormat("{0} pool does not exist", id);
    }



//    /// <summary>
//    /// добавить объект к уже существующему пулу
//    /// </summary>
//    /// <param name="id"></param>
//    /// <param name=""></param>
//    public void Add(PoolType id, GameObject prefab, int count = 1, Transform parent = null)
//    {
//        Pool pool = null;
//
//        if (_dictGameObject.TryGetValue((int) id, out pool))
//        {
//            for (int i = 0; i < count; i++)
//            {
//                var obj = Instantiate(prefab, transform);
//                obj.SetActive(false);
//                IPoollable ipoolable = obj.GetComponent<IPoollable>();
//                if(ipoolable != null) ipoolable.Init();
//                
//                pool.AddObject(obj, true);
//            }
//        }
//
//        else Debug.Log("Pool is not find");
//    }
    
    
    
    
    /// <summary>
    /// добавить объект к уже существующему пулу
    /// </summary>
    /// <param name="id"></param>
    /// <param name=""></param>
    public void Add(PoolType id, GameObject obj)
    {
        Pool pool = null;

        if (_dictGameObject.TryGetValue((int) id, out pool))
        {                
             pool.AddObject(obj, true);
        }

        else Debug.Log("Pool is not find");
    }

   
    public void MixObject(PoolType id)
    {
        Pool pool;
        if (_dictGameObject.TryGetValue((int) id, out pool))
        {
            var arr = GetStack(id).ToArray();
            GetStack(id).Clear();
            System.Random rnd = new System.Random();
            foreach (var value in arr.OrderBy(x => rnd.Next()))
                GetStack(id).Enqueue(value); 
        }
        else Debug.Log(id + " pool not exist");
    }


}



public enum PoolType
{
    Player,
    Enemy,
    ActiveEnemy,
    Ball
}
