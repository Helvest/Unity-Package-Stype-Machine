using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StypeMachine
{
	public class FlagMachine<T>
	{

		#region Fields

		public bool acceptFlagsNotIncluded = false;

		public bool canReenterSameFlag = false;

		private readonly Dictionary<T, FlagEventHandler> _flags = new Dictionary<T, FlagEventHandler>();

		#endregion

		#region Constructor

		public FlagMachine(bool acceptFlagsNotIncluded = false, bool canReenterSameFlag = false)
		{
			this.acceptFlagsNotIncluded = acceptFlagsNotIncluded;
			this.canReenterSameFlag = canReenterSameFlag;

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Constructor - acceptFlagsNotIncluded: " + acceptFlagsNotIncluded + " - canReenterSameFlag: " + canReenterSameFlag);
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

		#region Set Flags

		public bool SetFlag(T value)
		{
			if (value == null)
			{
				return false;
			}

			bool containsKey = _flags.ContainsKey(value);

			if (!canReenterSameFlag && containsKey)
			{
				return true;
			}

			bool valueFound = _flagEventHandlerDict.TryGetValue(value, out var newStateEventHandler);

			if (!acceptFlagsNotIncluded && !valueFound)
			{
				Debug.LogWarning("Set Flag - Value [" + value + "] not apcepted");
				return false;
			}

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Set Flag " + value);
			}
#endif

			if (!containsKey)
			{
				_flags.Add(value, newStateEventHandler);
			}

			if (valueFound)
			{
				newStateEventHandler.enterFlag?.Invoke(value);
			}

			return true;
		}

		public void SetFlags(params T[] flags)
		{
			foreach (var flag in flags)
			{
				SetFlag(flag);
			}
		}

		public void SetFlags(IEnumerable<T> flags)
		{
			foreach (var flag in flags)
			{
				SetFlag(flag);
			}
		}

		#endregion

		#region Unset Flags

		public bool UnsetFlag(T value)
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

			stateEventHandler?.exitFlag?.Invoke(value);

			return true;
		}

		public void UnsetFlags(params T[] flags)
		{
			foreach (var flag in flags)
			{
				UnsetFlag(flag);
			}
		}

		public void UnsetFlags(IEnumerable<T> flags)
		{
			foreach (var flag in flags)
			{
				UnsetFlag(flag);
			}
		}

		public void UnsetAllFlags()
		{
			var flags = _flags.Keys.ToList();

			foreach (var item in flags)
			{
				UnsetFlag(item);
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
					UnsetFlag(oldFlag);
				}
			}

			foreach (var newFlag in flags)
			{
				if (!oldFlags.Contains(newFlag))
				{
					SetFlag(newFlag);
				}
			}
		}

		#endregion

		#region Add Flags

		private class FlagEventHandler
		{
			public Action<T> enterFlag;

			public Action updateFlag;

			public Action<T> exitFlag;
		}

		private readonly Dictionary<T, FlagEventHandler> _flagEventHandlerDict = new Dictionary<T, FlagEventHandler>();

		public void AddFlag(T flag, Action<T> enterAction = default, Action<T> exitAction = default, Action updateAction = default)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Add Flag: " + flag
					+ "\nEnter: " + (enterAction != null ? $"{enterAction.Method.ReflectedType}.{enterAction.Method.Name}()" : "null")
					+ "\nExit: " + (exitAction != null ? $"{exitAction.Method.ReflectedType}.{exitAction.Method.Name}()" : "null")
					+ "\nUpdate: " + (updateAction != null ? $"{updateAction.Method.ReflectedType}.{updateAction.Method.Name}()" : "null")
				);
			}
#endif

			if (!_flagEventHandlerDict.TryGetValue(flag, out var stateEventHandler))
			{
				stateEventHandler = new FlagEventHandler();
				_flagEventHandlerDict.Add(flag, stateEventHandler);
			}

			stateEventHandler.enterFlag = enterAction;
			stateEventHandler.exitFlag = exitAction;
			stateEventHandler.updateFlag = updateAction;
		}

		#endregion

		#region Remove Flags

		public bool RemoveFlag(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Remove Flag: " + state);
			}
#endif

			return _flagEventHandlerDict.Remove(state);
		}

		public void RemoveAllFlag(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Remove Flag: " + state);
			}
#endif

			UnsetAllFlags();

			_flagEventHandlerDict.Clear();
		}

		#endregion

		#region Update Flags

		public void UpdateFlags()
		{
			foreach (var stateEventHandler in _flags.Values)
			{
				if (stateEventHandler != null)
				{
					stateEventHandler?.updateFlag?.Invoke();
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
