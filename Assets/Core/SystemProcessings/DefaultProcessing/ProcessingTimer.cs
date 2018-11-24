using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class ProcessingTimer : ITick
    {

        private List<Timer> _ticks;

        public ProcessingTimer()
        {
            _ticks = new List<Timer>();
        }


        public void Tick()
        {
            for (int i = 0; i < _ticks.Count; i++)
            {
                _ticks[i].Tick();
            }
        }

        public void Add(Timer timer)
        {
            _ticks.Add(timer);
        }

        public void Remove(Timer timer)
        {
            _ticks.Remove(timer);
        }
    }
}
