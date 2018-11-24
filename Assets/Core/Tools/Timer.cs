using System;
using Assets.Scripts.Tools;
using UnityEngine;


    public class Timer : IDisposable
    {
        private readonly float _finishTime;
        private float _curentTime;
        private  Action _callback;
        private readonly bool _repeat;


        public Timer(float timer, Action method, bool repeat)
        {
            _finishTime = timer;
            _callback += method;
            _repeat = repeat;
            _curentTime = 0.0f;
        }

        public static void Add(float finishTime, Action method, bool repeat = false)
        {
            var timer = new Timer(finishTime, method, repeat);
            GameManager.Instance.Systems.Get<ProcessingTimer>().Add(timer);
        }

        public void Tick()
        {
            _curentTime += Time.deltaTime;

            if (_curentTime > _finishTime)
            {
                _callback.Invoke();
               // Debug.Log("Callback name " + _callback.Target);
                
                if (_repeat)
                {
                    _curentTime = 0.0f;
                }

                else
                {
                    Kill();
                }
            }   
        }

        public void Stop()
        {
            GameManager.Instance.Systems.Get<ProcessingTimer>().Remove(this);
        }

        public void Play()
        {
            GameManager.Instance.Systems.Get<ProcessingTimer>().Add(this);
        }


    void Kill()
    {
        GameManager.Instance.Systems.Get<ProcessingTimer>().Remove(this);
        _callback = null;
    }

        public void Dispose()
        {
        }
    }
