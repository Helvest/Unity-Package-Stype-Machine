using System;
using System.Collections.Generic;
using UnityEngine;

namespace StypeMachine
{
	[Serializable]
	public class StateMachine<T>
	{

		#region Fields

		public T DefaultState { get; protected set; } = default;

		public bool acceptStateNotIncluded = false;

		public bool canReenterSameState = false;

		#endregion

		#region Constructor

		public StateMachine(T defaultValue = default, bool acceptStateNotIncluded = false, bool canReenterSameState = false)
		{
			this.acceptStateNotIncluded = acceptStateNotIncluded;
			this.canReenterSameState = canReenterSameState;
			DefaultState = defaultValue;

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Constructor - acceptStateNotIncluded: " + acceptStateNotIncluded + ", DefaultValue: " + defaultValue);
			}
#endif

			if (defaultValue != null)
			{
				AddState(defaultValue);
				_state = defaultValue;
			}
		}

		#endregion

		#region Get Set State

		public bool IsState(T state)
		{
			return _state != null ? _state.Equals(state) : state == null;
		}

		private T _state;

		public T State
		{
			get => _state;

			set
			{
				if (value == null)
				{
					return;
				}

				// if the "new state" is the current one, we do nothing and exit
				if (!canReenterSameState && value.Equals(_state))
				{
#if UNITY_EDITOR
					if (useDebug)
					{
						Debug.Log("Set State - Same state [" + value + "], do nothing");
					}
#endif
					return;
				}

				bool valueFound = _stateEventHandlerDict.TryGetValue(value, out var newStateEventHandler);

				if (!acceptStateNotIncluded && !valueFound)
				{
					Debug.LogWarning("Set State - Value [" + value + "] not apcepted");
					return;
				}

				_stateEventHandler?.ExitState?.Invoke(_state, value);

				var previousState = _state;
				_state = value;

#if UNITY_EDITOR
				if (useDebug)
				{
					if (previousState == null)
					{
						Debug.Log("Set State to " + _state);
					}
					else
					{
						Debug.Log("Set State from " + previousState + " to " + _state);
					}

				}
#endif

				if (valueFound)
				{
					_stateEventHandler = newStateEventHandler;
					_stateEventHandler.EnterState?.Invoke(previousState, value);
				}
				else
				{
					_stateEventHandler = default;
				}
			}
		}

		#endregion

		#region Add Remove State

		private class StateEventHandler
		{
			public Action<T, T> EnterState;

			public Action UpdateState;

			public Action<T, T> ExitState;
		}

		private StateEventHandler _stateEventHandler;

		private readonly Dictionary<T, StateEventHandler> _stateEventHandlerDict = new Dictionary<T, StateEventHandler>();

		public void AddState(T state, Action<T, T> enterAction = default, Action<T, T> exitAction = default, Action updateAction = default)
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

			stateEventHandler.EnterState = enterAction;
			stateEventHandler.ExitState = exitAction;
			stateEventHandler.UpdateState = updateAction;
		}

		public void RemoveState(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Remove State: " + state);
			}
#endif

			_stateEventHandlerDict.Remove(state);
		}

		public void UpdateState()
		{
			_stateEventHandler?.UpdateState?.Invoke();
		}

		#endregion

		#region Default State

		public void ToDefaultState()
		{
			State = DefaultState;
		}

		public virtual void SetDefaultState(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Set Default State: " + state);
			}
#endif

			DefaultState = state;

			if (state != null && !_stateEventHandlerDict.ContainsKey(state))
			{
				AddState(state);
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

