﻿using System;
using System.Collections.Generic;
using UnityEngine;


	[Serializable]
	public class ProcessingEvent : Singleton<ProcessingEvent>
	{
		public static ProcessingEvent Default;
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

		public void Add<T>(IRecieve recieve)
		{
			List<IRecieve> cachedSignals;
			if (signals.TryGetValue(typeof(T).GetHashCode(), out cachedSignals))
			{
				cachedSignals.Add(recieve);
				return;
			}

			signals.Add(typeof(T).GetHashCode(), new List<IRecieve> {recieve});
		}

		public void Remove<T>(IRecieve recieve)
		{
			List<IRecieve> cachedSignals;
			Timer.Add(Time.deltaTime, () =>
			{
				if (signals.TryGetValue(typeof(T).GetHashCode(), out cachedSignals))
				{
					cachedSignals.Remove(recieve);
				}
			});
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


		public void Add(object obj)
		{
			var reciever = obj as IRecieve;
			if (reciever == null) return;
       
			var all = obj.GetType().GetInterfaces();

			foreach (var intType in all)
			{
				if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IReceiveGlobal<>))
				{
					Default.Add(reciever, intType.GetGenericArguments()[0]);
				}
				else if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IReceive<>))
				{
			 
				 
					Add(reciever, intType.GetGenericArguments()[0]);
				}
			}
		}

		public void Remove(object obj)
		{
			var reciever = obj as IRecieve;
			if (reciever == null) return;
			var all = obj.GetType().GetInterfaces();

			foreach (Type intType in all)
			{
				if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IReceiveGlobal<>))
				{
					Default.Remove(reciever, intType.GetGenericArguments()[0]);
				}
				else if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IReceive<>))
				{
					Remove(reciever, intType.GetGenericArguments()[0]);
				}
			}
		}

		public void Dispose()
		{
			signals.Clear();
		}

		#endregion
	}
