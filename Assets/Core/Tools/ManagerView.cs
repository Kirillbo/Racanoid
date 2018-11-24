using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ManagerView : IDisposable {

    private static Dictionary<Type, MonoBehaviour> _views = new Dictionary<Type, MonoBehaviour>();

    public static T Get<T>() where T : MonoBehaviour
    {
        
        var keyType = typeof(T);

        if (_views.ContainsKey(keyType))
        {
            return _views[keyType] as T;
        }

        var result = Object.FindObjectOfType(keyType) as T;
        if (result == null)
        {
            Debug.LogFormat("Object type {0} not find on scene", keyType);
            return null;
        }
        _views.Add(keyType, result);
        return result;
        
    }


    public void Dispose()
    {
        _views.Clear();
    }
}
