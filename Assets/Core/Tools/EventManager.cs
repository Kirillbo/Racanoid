using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


[System.Serializable]
public class MyEvent : UnityEvent<GameObject, string>
{

}


public class EventManager : MonoBehaviour
{
    // Singleton

    // Events list
    private Dictionary<string, MyEvent> eventDictionary = new Dictionary<string, MyEvent>();

    public static EventManager Instantiate;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    protected void Awake()
    {

        if (Instantiate == null)
        {
            Instantiate = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instantiate != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        if (Instantiate == this)
        {
            Instantiate = null;
        }
    }

    /// <summary>
    /// Start listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StartListening(string eventName, UnityAction<GameObject, string> listener)
    {
        if (Instantiate == null)
        {
            Instantiate = FindObjectOfType(typeof(EventManager)) as EventManager;
            if (Instantiate == null)
            {
                Debug.Log("Have no event manager on scene");
                return;
            }
        }

        MyEvent thisEvent = null;
        if (Instantiate.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new MyEvent();
            thisEvent.AddListener(listener);
            Instantiate.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stop listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StopListening(string eventName, UnityAction<GameObject, string> listener)
    {
        if (Instantiate == null)
        {
            return;
        }
        MyEvent thisEvent = null;
        if (Instantiate.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(string eventName, string param, GameObject obj = null)
    {
        if (Instantiate == null)
        {
            return;
        }
        MyEvent thisEvent = null;

        if (Instantiate.eventDictionary.TryGetValue(eventName, out thisEvent))
        {   
            thisEvent.Invoke(obj, param);
        }

        else Debug.Log("This event not find");
    }
}
