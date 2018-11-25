using System;
using System.Collections.Generic;
using UnityEngine;


public class SystemProcessings 
    {
        private Dictionary<int, object> _data;
        private List<ITick> _listTicks;
        private List<IFixTick> _listFixTicks;
        
        public bool SystemChange;

        public SystemProcessings()
        {
            _listTicks = new List<ITick>();
            _listFixTicks = new List<IFixTick>();
            _data = new Dictionary<int, object>();
            SystemChange = false;
            
        }

        public bool Contains<T>()
        {
            return _data.ContainsKey(typeof(T).GetHashCode());
        }


        public T Add<T>(Type type = null) where T : new()
        {
            object o;
            var hash = type == null ? typeof(T).GetHashCode() : type.GetHashCode();
            if (_data.TryGetValue(hash, out o))
            {
                InitializeObject(o);
                return (T)o;
            }

            var created = new T();

            InitializeObject(created);
            _data.Add(hash, created);

            return created;
        }

        public object Add(object obj)
        {
            object needObject;
            var key = obj.GetType().GetHashCode();

            if (!_data.TryGetValue(key, out needObject))
            {
                if (obj is ScriptableObject)
                {
                    needObject = MonoBehaviour.Instantiate((ScriptableObject)obj);
                    _data.Add(key, needObject);
                    return needObject;
                }
            }

            Debug.Log("This Object is instance");
            return null;
        }

//        public Type Get(object obj)
//        {
//            object needObject;
//            var key = obj.GetType().GetHashCode();
//
//            if (_data.TryGetValue(key, out needObject))
//            {
//                return needObject.GetType();
//            }
//            
//            Debug.Log("Object not find");
//            return null;
//        }
        
        public object Get(Type t)
        {
            object resolve;
            _data.TryGetValue(t.GetHashCode(), out resolve);
            return resolve;
        }

        public object Get(object obj)
        {
            object resolve;
            _data.TryGetValue(obj.GetType().GetHashCode(), out resolve);
            return resolve;
        }


        public void FixUpdate()
        {
            if(SystemChange || _listFixTicks.Count == 0) return;

            for (int i = 0; i < _listFixTicks.Count; i++)
            {
                _listFixTicks[i].FixTick();
            }
        }
        
        public void Update()
        {
            if(SystemChange || _listTicks.Count == 0) return;

            for (int i = 0; i < _listTicks.Count; i++)
            {
                _listTicks[i].Tick();
            }
        }

        public object Remove(object obj)
        {
            return _data.Remove(obj.GetType().GetHashCode());
        }

        public void InitializeObject(object obj)
        {
            var awakeble = obj as IAwake;
            if (awakeble != null) awakeble.OnAwake();

            var tickable = obj as ITick;
            if (tickable != null) _listTicks.Add(tickable);

            var fixTicable = obj as IFixTick;
            if (fixTicable != null) _listFixTicks.Add(fixTicable);
        }


        public  T Get<T>()
        {
            object resolve;

            var hasValue = _data.TryGetValue(typeof(T).GetHashCode(), out resolve);

            if (!hasValue)
                _data.TryGetValue(typeof(T).GetHashCode(), out resolve);
            return (T)resolve;
        }

        public void Clear()
        {
            SystemChange = true;
            foreach (var pair in _data)
            { 
                var needToBeCleaned = pair.Value as IDisposable;
                if (needToBeCleaned == null) continue;
                needToBeCleaned.Dispose();
            }

            _listTicks.Clear();
            _data.Clear();

        }
    }
