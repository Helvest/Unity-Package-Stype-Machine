using System;
using System.Collections.Generic;
using UnityEngine;

namespace StypeMachine
{
	[Serializable]
	public class StateMachine<T>
	{

		#region Fields

		private T _state;

		public bool acceptStatesNotIncluded = false;

		public bool canReenterSameState = false;

		#endregion

		#region Constructor

		public StateMachine(T firstState, bool acceptStateNotIncluded = false, bool canReenterSameState = false)
		{
			this.acceptStatesNotIncluded = acceptStateNotIncluded;
			this.canReenterSameState = canReenterSameState;

#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("Constructor - acceptStateNotIncluded: " + acceptStateNotIncluded + ", firstState: " + firstState);
			}
#endif

			_state = firstState;
			AddState(firstState);
		}

		#endregion

		#region Get Set State

		public bool IsState(T state)
		{
			return _state != null ? _state.Equals(state) : state == null;
		}

		public T State
		{
			get => _state;

			set
			{
				if (value == null)
				{
#if UNITY_EDITOR
					if (useDebug)
					{
						Debug.Log("Set State - Can not be null");
					}
#endif
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

				if (!acceptStatesNotIncluded && !valueFound)
				{
					Debug.LogWarning("Set State - Value [" + value + "] not apcepted");
					return;
				}

				_stateEventHandler?.exitState?.Invoke(_state, value);

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
					_stateEventHandler.enterState?.Invoke(previousState, value);
				}
				else
				{
					_stateEventHandler = default;
				}
			}
		}

		#endregion

		#region StateEventHandler

		private class StateEventHandler
		{
			public Action<T, T> enterState;

			public Action updateState;

			public Action<T, T> exitState;
		}

		private StateEventHandler _stateEventHandler;

		private readonly Dictionary<T, StateEventHandler> _stateEventHandlerDict = new Dictionary<T, StateEventHandler>();

		#endregion

		#region Add State

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

			stateEventHandler.enterState = enterAction;
			stateEventHandler.exitState = exitAction;
			stateEventHandler.updateState = updateAction;

			//If state is already actif, call the enterAction
			if (state.Equals(_state))
			{
				enterAction?.Invoke(state, state);
			}
		}

		#endregion

		#region Remove State

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

		#endregion

		#region Update State

		public void UpdateState()
		{
			_stateEventHandler?.updateState?.Invoke();
		}

		#endregion

		#region Debug

#if UNITY_EDITOR
		public bool useDebug = false;
#endif

		#endregion

	}
}
