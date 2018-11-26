using UnityEngine;

public class SingltoonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
