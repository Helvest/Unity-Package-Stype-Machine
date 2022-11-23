using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StypeMachine
{
	public class FlagMachine<T>
	{

		#region Fields

		public bool acceptStateNotIncluded = false;

		public bool canReenterSameState = false;

		private readonly Dictionary<T, StateEventHandler> _flags = new Dictionary<T, StateEventHandler>();

		#endregion

		#region Constructor

		public FlagMachine(bool acceptStateNotIncluded = false, bool canReenterSameState = false)
		{
			this.acceptStateNotIncluded = acceptStateNotIncluded;
			this.canReenterSameState = canReenterSameState;

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Constructor - acceptStateNotIncluded: " + acceptStateNotIncluded + " - canReenterSameState: " + canReenterSameState);
			}
#endif
		}

		#endregion

		#region Has Flags

		public bool HasFlag(T flag)
		{
			return _flags.ContainsKey(flag);
		}

		public bool HasFlags(params T[] flags)
		{
			foreach (var flag in flags)
			{
				if (!_flags.ContainsKey(flag))
				{
					return false;
				}
			}
			return true;
		}

		public bool HasFlags(IEnumerable<T> flags)
		{
			foreach (var flag in flags)
			{
				if (!_flags.ContainsKey(flag))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Get Flags

		public List<T> GetFlags()
		{
			return _flags.Keys.ToList();
		}

		#endregion

		#region Add Flags

		public bool AddFlag(T value)
		{
			if (value == null)
			{
				return false;
			}

			bool containsKey = _flags.ContainsKey(value);

			if (!canReenterSameState && containsKey)
			{
				return true;
			}

			bool valueFound = _stateEventHandlerDict.TryGetValue(value, out var newStateEventHandler);

			if (!acceptStateNotIncluded && !valueFound)
			{
				Debug.LogWarning("Set State - Value [" + value + "] not apcepted");
				return false;
			}

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Add Flag " + value);
			}
#endif

			if (!containsKey)
			{
				_flags.Add(value, newStateEventHandler);
			}

			if (valueFound)
			{
				newStateEventHandler.enterState?.Invoke(value);
			}

			return true;
		}

		public void AddFlags(params T[] flags)
		{
			foreach (var flag in flags)
			{
				AddFlag(flag);
			}
		}

		public void AddFlags(IEnumerable<T> flags)
		{
			foreach (var flag in flags)
			{
				AddFlag(flag);
			}
		}

		#endregion

		#region Remove Flags

		public bool RemoveFlag(T value)
		{
			if (!_flags.TryGetValue(value, out var stateEventHandler))
			{
				return false;
			}

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Remove Flag " + value);
			}
#endif

			_flags.Remove(value);

			stateEventHandler?.exitState?.Invoke(value);

			return true;
		}

		public void RemoveFlags(params T[] flags)
		{
			foreach (var flag in flags)
			{
				RemoveFlag(flag);
			}
		}

		public void RemoveFlags(IEnumerable<T> flags)
		{
			foreach (var flag in flags)
			{
				RemoveFlag(flag);
			}
		}

		public void RemoveAllFlags()
		{
			var flags = _flags.Keys.ToList();

			foreach (var item in flags)
			{
				RemoveFlag(item);
			}
		}

		#endregion

		#region Replace Flags

		public void ReplaceFlags(params T[] flags)
		{
			ReplaceFlagsIntern(flags.ToList());
		}

		public void ReplaceFlags(IEnumerable<T> flags)
		{
			ReplaceFlagsIntern(flags);
		}

		protected void ReplaceFlagsIntern(IEnumerable<T> flags)
		{
			var oldFlags = _flags.Keys.ToList();

			foreach (var oldFlag in oldFlags)
			{
				if (!flags.Contains(oldFlag))
				{
					RemoveFlag(oldFlag);
				}
			}

			foreach (var newFlag in flags)
			{
				if (!oldFlags.Contains(newFlag))
				{
					AddFlag(newFlag);
				}
			}
		}

		#endregion

		#region Add Remove State

		private class StateEventHandler
		{
			public Action<T> enterState;

			public Action updateState;

			public Action<T> exitState;
		}

		private readonly Dictionary<T, StateEventHandler> _stateEventHandlerDict = new Dictionary<T, StateEventHandler>();

		public void AddState(T state, Action<T> enterAction = default, Action<T> exitAction = default, Action updateAction = default)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Add State: " + state
					+ "\nEnter: " + (enterAction != null ? $"{enterAction.Method.ReflectedType}.{enterAction.Method.Name}()" : "null")
					+ "\nExit: " + (exitAction != null ? $"{exitAction.Method.ReflectedType}.{exitAction.Method.Name}()" : "null")
					+ "\nUpdate: " + (updateAction != null ? $"{updateAction.Method.ReflectedType}.{updateAction.Method.Name}()" : "null")
				);
			}
#endif

			if (!_stateEventHandlerDict.TryGetValue(state, out var stateEventHandler))
			{
				stateEventHandler = new StateEventHandler();
				_stateEventHandlerDict.Add(state, stateEventHandler);
			}

			stateEventHandler.enterState = enterAction;
			stateEventHandler.exitState = exitAction;
			stateEventHandler.updateState = updateAction;
		}

		public bool RemoveState(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Remove State: " + state);
			}
#endif

			return _stateEventHandlerDict.Remove(state);
		}

		public void UpdateState()
		{
			foreach (var stateEventHandler in _flags.Values)
			{
				if (stateEventHandler != null)
				{
					stateEventHandler?.updateState?.Invoke();
				}
			}
		}

		#endregion

		#region Debug

#if UNITY_EDITOR
		public bool useDebug = false;
#endif

		#endregion

	}
}
