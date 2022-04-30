using System;
using UnityEngine;

namespace StypeMachine
{
	[DefaultExecutionOrder(-9999)]
	public abstract class TypeMachineController<T> : MonoBehaviour, IHoldTypeMachine where T : class
	{

		#region Fields

		[Header("TypeMachineController")]

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
			TypeMachine.ToDefaultState();
		}

		protected virtual void ToStartState()
		{
			if (!hasStarted || startState == null)
			{
				return;
			}

			this.SetState(startState);
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
