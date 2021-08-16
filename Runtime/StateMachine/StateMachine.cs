using System;
using System.Collections.Generic;
using UnityEngine;

namespace StypeMachine
{
	[Serializable]
	public class StateMachine<T>
	{

		#region Variables

		public T DefaultValue { get; protected set; } = default;

		public bool apceptValuesNotIncluded = false;

		public bool canReenterSameState = false;

		#endregion

		#region Constructor

		public StateMachine(T defaultValue = default, bool apceptValuesNotIncluded = false, bool canReenterSameState = false)
		{
			this.apceptValuesNotIncluded = apceptValuesNotIncluded;
			this.canReenterSameState = canReenterSameState;
			DefaultValue = defaultValue;

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Constructor - apceptValuesNotIncluded: " + apceptValuesNotIncluded + ", DefaultValue: " + defaultValue);
			}
#endif

			if (defaultValue != null)
			{
				AddState(defaultValue);
			}
		}

		#endregion

		#region Get Set State

		private T _state;

		public T State
		{
			get
			{
				return _state;
			}

			set
			{
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

#if UNITY_EDITOR
				if (useDebug)
				{
					Debug.Log("Set State - " + value + " => " + _state);
				}
#endif

				bool valueFound = _stateEventHandlerDict.TryGetValue(value, out var newStateEventHandler);

				if (!apceptValuesNotIncluded && !valueFound)
				{
					Debug.LogWarning("Value [" + value + "] not apcepted");
					return;
				}

				_stateEventHandler?.ExitState?.Invoke();

				_state = value;

				if (valueFound)
				{
					_stateEventHandler = newStateEventHandler;
					_stateEventHandler.EnterState?.Invoke();
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
			public Action EnterState;

			public Action UpdateState;

			public Action ExitState;
		}

		private StateEventHandler _stateEventHandler;

		private readonly Dictionary<T, StateEventHandler> _stateEventHandlerDict = new Dictionary<T, StateEventHandler>();

		public void AddState(T state, Action enterAction = default, Action exitAction = default, Action updateAction = default)
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

		public void ToDefault()
		{
			State = DefaultValue;
		}

		public virtual void SetDefaultState(T state)
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Set Default State: " + state);
			}
#endif

			DefaultValue = state;

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

