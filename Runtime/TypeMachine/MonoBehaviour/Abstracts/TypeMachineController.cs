using System;
using UnityEngine;

namespace StypeMachine
{
	public abstract class TypeMachineController<T> : MonoBehaviour
	{

		#region Variables

		[SerializeField]
		protected bool apceptValuesNotIncluded = false;

		[SerializeField]
		protected bool canReenterSameState = false;

		[SerializeField]
		protected T startState = default;

		public TypeMachine TypeMachine { get; private set; }

		protected bool hasStarted = false;

		#endregion

		#region State

		public void SetState<t>() where t : T
		{
			TypeMachine.SetState<t>();
		}

		public void SetState<t>(t instance) where t : T
		{
			TypeMachine.SetState(instance);
		}

		public void SetState(Type type)
		{
			TypeMachine.SetState(type);
		}

		public virtual Type State
		{
			get => TypeMachine.State;

			set => TypeMachine.State = value;
		}

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateTypeMachine();

#if UNITY_EDITOR
			TypeMachine.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartState();
		}

		protected virtual void CreateTypeMachine()
		{
			TypeMachine = new TypeMachine(typeof(T), apceptValuesNotIncluded, canReenterSameState);
		}

		protected virtual void OnEnable()
		{
			ToStartState();
		}

		protected virtual void OnDisable()
		{
			TypeMachine.ToDefault();
		}

		protected virtual void ToStartState()
		{
			if (!hasStarted || startState == null)
			{
				return;
			}

			SetState(startState);
		}

		#endregion

		#region AddState

		public void AddState<t>(Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default) where t : T
		{
			TypeMachine.AddState<t>(enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t state, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default) where t : T
		{
			TypeMachine.AddState(state, enterAction, exitAction, updateAction);
		}

		#endregion

		#region RemoveState

		public void RemoveState<t>() where t : T
		{
			TypeMachine.RemoveState<t>();
		}

		public void RemoveState(T state)
		{
			TypeMachine.RemoveState(state);
		}

		#endregion

		#region SetDefaultState

		public void SetDefaultState<t>() where t : T
		{
			TypeMachine.SetDefaultState<t>();
		}

		public void SetDefaultState<t>(t state) where t : T
		{
			TypeMachine.SetDefaultState(state);
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

