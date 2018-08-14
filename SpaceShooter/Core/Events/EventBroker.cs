using System;
using System.Collections.Generic;

namespace SpaceShooter.Core.Events {
	/// <summary>
	/// Simple event dispatching system used to decouple system.
	/// </summary>
	internal static class EventBroker {
		private static readonly Dictionary<Type, List<Subscriber>> Subscriptions = new Dictionary<Type, List<Subscriber>>();

		public static void Register<T>(Action<T> callback) {
			if(!Subscriptions.TryGetValue(typeof(T), out var list))
				Subscriptions[typeof(T)] = list = new List<Subscriber>();
			list.Add(new Subscriber<T>(callback));
		}

		public static void Unregister<T>(Action<T> callback) {
			if(Subscriptions.TryGetValue(typeof(T), out var list))
				list.RemoveAll(x => x is Subscriber<T> sub && sub.Callback == callback);
		}

		public static void Clear() {
			Subscriptions.Clear();
		}

		public static void Dispatch<T>(T eventData) {
			if(!Subscriptions.TryGetValue(typeof(T), out var list)) return;
			for(int i = list.Count - 1; i >= 0; i--) {
				list[i].Invoke(eventData);
			}
		}

		private abstract class Subscriber {
			public abstract void Invoke(object obj);
		}

		private class Subscriber<T> : Subscriber {
			public readonly Action<T> Callback;

			public Subscriber(Action<T> callback) {
				Callback = callback;
			}

			public override void Invoke(object obj) {
				Callback((T)obj);
			}
		}
	}
}
