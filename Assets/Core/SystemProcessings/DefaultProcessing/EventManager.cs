using System;
using System.Collections.Generic;
using UnityEngine;


	public class EventManager : Singleton<EventManager>, IDisposable
	{
		
		public readonly Dictionary<int, List<IRecieve>> signals = new Dictionary<int, List<IRecieve>>();

		#region LOGIC

		public void Send<T>(T val = default(T))
		{
			List<IRecieve> cachedSignals;

			if (!signals.TryGetValue(typeof(T).GetHashCode(), out cachedSignals)) return;

			var len = cachedSignals.Count;
		 
			for (int i = 0; i < len; i++)
			{
				(cachedSignals[i] as IReceive<T>).HandleSignal(val);
			}
		}

		public EventManager Add<T>(IRecieve recieve)
		{
			List<IRecieve> cachedSignals;
			if (signals.TryGetValue(typeof(T).GetHashCode(), out cachedSignals))
			{
				cachedSignals.Add(recieve);
				return this;
			}

			signals.Add(typeof(T).GetHashCode(), new List<IRecieve> {recieve});
			return this;
		}

		public EventManager Remove<T>(IRecieve recieve)
		{
			List<IRecieve> cachedSignals;
			Timer.Add(Time.deltaTime, () =>
			{
				if (signals.TryGetValue(typeof(T).GetHashCode(), out cachedSignals))
				{
					cachedSignals.Remove(recieve);
					
				}
			});

			return this;
		}


		public void Add(IRecieve recieve, Type type)
		{
			List<IRecieve> cachedSignals;
			if (signals.TryGetValue(type.GetHashCode(), out cachedSignals))
			{
				cachedSignals.Add(recieve);
				return;
			}

			signals.Add(type.GetHashCode(), new List<IRecieve> {recieve});
		}

		public void Remove(IRecieve recieve, Type type)
		{
			List<IRecieve> cachedSignals;
			Timer.Add(Time.deltaTime, () =>
			{
				if (signals.TryGetValue(type.GetHashCode(), out cachedSignals))
				{
					cachedSignals.Remove(recieve);
				}
			});
		}


		public void Dispose()
		{
			signals.Clear();
			_instance = null;
		}

		#endregion
	}
