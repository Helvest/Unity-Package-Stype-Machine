using System;
using UnityEngine;

namespace StypeMachine
{
	public abstract class TypeMachineController<T> : MonoBehaviour
	{

		#region Variables

		[SerializeField]
		protected bool _apceptValuesNotIncluded = false;

		[SerializeField]
		protected bool _canReenterSameState = false;

		[SerializeField]
		protected T _startState = default;

		protected TypeMachine typeMachine;

		#endregion

		#region State

		public void SetState<t>() where t : T
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("SetState: " + typeof(t));
			}
#endif

			typeMachine.SetState<t>();
		}

		public void SetState<t>(t type) where t : T
		{
#if UNITY_EDITOR
			if (useDebug)
			{
				Debug.Log("SetState: " + type.GetType());
			}
#endif

			typeMachine.SetState(type);
		}

		public virtual Type State
		{
			get
			{
				return typeMachine.State;
			}

			set
			{
				typeMachine.State = value;
			}
		}

		#endregion

		#region Init

		protected virtual void Awake()
		{
			typeMachine = new TypeMachine(typeof(T), _apceptValuesNotIncluded, _canReenterSameState);
		}

		protected virtual void OnEnable()
		{
			if (_startState != null)
			{
				SetState(_startState);
			}
		}

		protected virtual void OnDisable()
		{
			typeMachine.ToDefault();
		}

		#endregion

		#region AddState

		public void AddState<t>(Action enterAction = default, Action exitAction = default, Action updateAction = default) where t : T
		{
			typeMachine.AddState<t>(enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t state, Action enterAction = default, Action exitAction = default, Action updateAction = default) where t : T
		{
			typeMachine.AddState(state, enterAction, exitAction, updateAction);
		}

		#endregion

		#region RemoveState

		public void RemoveState<t>() where t : T
		{
			typeMachine.RemoveState<t>();
		}

		public void RemoveState(T state)
		{
			typeMachine.RemoveState(state);
		}

		#endregion

		#region SetDefaultState

		public void SetDefaultState<t>() where t : T
		{
			typeMachine.SetDefaultState<t>();
		}

		public void SetDefaultState<t>(t state) where t : T
		{
			typeMachine.SetDefaultState(state);
		}

		#endregion

		#region Debug

#if UNITY_EDITOR
		[Header("Debug")]
		[SerializeField]
		protected bool useDebug = false;
#endif

		#endregion

	}

}

